using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using PetDiverse.Data;
using PetDiverse.Data.Enums;
using PetDiverse.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetDiverse.Controllers
{
    public class PessoaDoadoraController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PessoaDoadoraController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "ADMIN")]
        // GET: PessoaDoadora
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetForTable()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var pessoas = await _context.PessoaDoadora.ToListAsync();
            var result = new List<object>();
            foreach (var p in pessoas)
            {
                var usuario = await _userManager.FindByIdAsync(p.IdUsuario);
                bool isAdmin = usuario != null && await _userManager.IsInRoleAsync(usuario, "ADMIN");
                result.Add(new
                {
                    p.Id,
                    p.Nome,
                    p.IdUsuario,
                    DataExclusao = p.DataExclusao.HasValue ? p.DataExclusao.Value.ToString("dd/MM/yyyy") : (string?)null,
                    Tipo = p is PessoaFisica ? "Pessoa Física" : "Pessoa Jurídica",
                    IsAdmin = isAdmin,
                    IsSelf = p.IdUsuario == currentUserId
                });
            }
            return Json(result);
        }


        // GET: PessoaDoadora/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaDoadora = await _context.PessoaDoadora
                .Include(p => p.Bairro)
                    .ThenInclude(b => b.Cidade)
                        .ThenInclude(c => c.Estado)
                .Include(p => p.FormasContato)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pessoaDoadora == null)
            {
                return NotFound();
            }

            // Busca o email pelo UserManager
            var usuario = await _userManager.FindByIdAsync(pessoaDoadora.IdUsuario);
            ViewData["Email"] = usuario?.Email;

            return View(pessoaDoadora);
        }

        // GET: PessoaDoadora/Create
        public IActionResult Create()
        {
            ViewData["IdEstado"] = new SelectList(_context.Estado.OrderBy(e => e.Nome), "Id", "Nome");
            ViewData["IdCidade"] = new SelectList(new List<Cidade>());
            ViewData["IdBairro"] = new SelectList(new List<Bairro>());
            var listaTipoContato = Enum.GetValues(typeof(TipoFormaContato)).Cast<TipoFormaContato>().Select(e => new{ Valor = e,
                Nome = e.ToString()});
            ViewData["TiposFormaContato"] = new SelectList(listaTipoContato, "Valor","Nome");
            var listaTipoPessoa = Enum.GetValues(typeof(TipoPessoaCadastro)).Cast<TipoPessoaCadastro>().Select(e => new {
                Valor = e,
                Nome = e.ToString()
            });
            ViewData["TipoPessoaCadastro"] = new SelectList(listaTipoPessoa, "Valor", "Nome");
            return View();
        }

        // POST: PessoaDoadora/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PessoaDoadoraCadastroViewModel pessoaDoadoraCadastroViewModel)
        {
            ObrigatoriedadePessoaDoadora(pessoaDoadoraCadastroViewModel);
            if (ModelState.IsValid)
            {
                var pessoaDoadora = new PessoaDoadora();
                if(pessoaDoadoraCadastroViewModel.TipoPessoaCadastro == TipoPessoaCadastro.Fisica)
                {
                    var pessoaDoadoraFisica = new PessoaFisica();
                    pessoaDoadoraFisica.CPF = pessoaDoadoraCadastroViewModel.CPF;
                    pessoaDoadoraFisica.DataNascimento = pessoaDoadoraCadastroViewModel.DataNascimento;
                    pessoaDoadora = pessoaDoadoraFisica;
                }
                else
                {
                    var pessoaDoadoraJuridica = new PessoaJuridica();
                    pessoaDoadoraJuridica.CNPJ = pessoaDoadoraCadastroViewModel.CNPJ;
                    pessoaDoadoraJuridica.Site = pessoaDoadoraCadastroViewModel.Site;
                    pessoaDoadoraJuridica.RedeSocial = pessoaDoadoraCadastroViewModel.Site;
                    pessoaDoadora = pessoaDoadoraJuridica;
                }
                pessoaDoadora.Nome = pessoaDoadoraCadastroViewModel.Nome;
                pessoaDoadora.FormasContato = pessoaDoadoraCadastroViewModel.TipoFormaContato.Select(t => new FormaContato { TipoFormaContato = t }).ToList();
                pessoaDoadora.Telefone = pessoaDoadoraCadastroViewModel.Telefone;
                pessoaDoadora.IdBairro = pessoaDoadoraCadastroViewModel.IdBairro;
                pessoaDoadora.IdUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(pessoaDoadora);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["IdEstado"] = new SelectList(_context.Estado.OrderBy(e => e.Nome), "Id", "Nome", pessoaDoadoraCadastroViewModel.IdEstado);
            ViewData["IdCidade"] = new SelectList(_context.Cidade.Where(c => c.IdEstado == pessoaDoadoraCadastroViewModel.IdEstado).OrderBy(c => c.Nome), "Id", "Nome", pessoaDoadoraCadastroViewModel.IdCidade);
            ViewData["IdBairro"] = new SelectList(_context.Bairro.Where(b => b.IdCidade == pessoaDoadoraCadastroViewModel.IdCidade).OrderBy(b => b.Nome), "Id", "Nome", pessoaDoadoraCadastroViewModel.IdBairro);
            var listaTipoContato = Enum.GetValues(typeof(TipoFormaContato)).Cast<TipoFormaContato>().Select(e => new {
                Valor = e,
                Nome = e.ToString()
            });
            ViewData["TiposFormaContato"] = new SelectList(listaTipoContato, "Valor", "Nome");
            var listaTipoPessoa = Enum.GetValues(typeof(TipoPessoaCadastro)).Cast<TipoPessoaCadastro>().Select(e => new {
                Valor = e,
                Nome = e.ToString()
            });
            ViewData["TipoPessoaCadastro"] = new SelectList(listaTipoPessoa, "Valor", "Nome");
            return View(pessoaDoadoraCadastroViewModel);
        }

        // GET: PessoaDoadora/Edit/5
        public async Task<IActionResult> Edit(string idUsuario)
        {
            if (string.IsNullOrEmpty(idUsuario))
            {
                return NotFound();
            }

            var pessoaDoadora = await _context.PessoaDoadora
                .Include(p => p.Bairro)
                .ThenInclude(b => b.Cidade)
                .FirstOrDefaultAsync(p => p.IdUsuario == idUsuario);

            if (pessoaDoadora == null)
            {
                return NotFound();
            }

            var pessoaDoadoraCadastroViewModel = new PessoaDoadoraCadastroViewModel
            {
                Id = pessoaDoadora.Id,
                IdUsuario = pessoaDoadora.IdUsuario,
                Nome = pessoaDoadora.Nome,
                IdBairro = pessoaDoadora.IdBairro,
                TipoFormaContato = pessoaDoadora.FormasContato.Select(fc => fc.TipoFormaContato).ToList(),
                Telefone = pessoaDoadora.Telefone,
                IdEstado = pessoaDoadora.Bairro.Cidade.IdEstado,
                IdCidade = pessoaDoadora.Bairro.IdCidade,
                TelaAdmin = !string.IsNullOrWhiteSpace(Request.Headers["Referer"].ToString()) ? Request.Headers["Referer"].ToString().ToLower().Contains("pessoadoadora") : false,
            };

            if (pessoaDoadora is PessoaFisica pessoaDoadoraFisica)
            {
                pessoaDoadoraCadastroViewModel.CPF = pessoaDoadoraFisica.CPF;
                pessoaDoadoraCadastroViewModel.DataNascimento = pessoaDoadoraFisica.DataNascimento;
                pessoaDoadoraCadastroViewModel.TipoPessoaCadastro = TipoPessoaCadastro.Fisica;
            }
            else if (pessoaDoadora is PessoaJuridica pessoaDoadoraJuridica)
            {
                pessoaDoadoraCadastroViewModel.CNPJ = pessoaDoadoraJuridica.CNPJ;
                pessoaDoadoraCadastroViewModel.Site = pessoaDoadoraJuridica.Site;
                pessoaDoadoraCadastroViewModel.RedeSocial = pessoaDoadoraJuridica.RedeSocial;
                pessoaDoadoraCadastroViewModel.TipoPessoaCadastro = TipoPessoaCadastro.Juridica;
            }

            ViewData["IdEstado"] = new SelectList(_context.Estado.OrderBy(e => e.Nome), "Id", "Nome", pessoaDoadoraCadastroViewModel.IdEstado);
            ViewData["IdCidade"] = new SelectList(_context.Cidade.Where(c => c.IdEstado == pessoaDoadoraCadastroViewModel.IdEstado).OrderBy(c => c.Nome), "Id", "Nome", pessoaDoadoraCadastroViewModel.IdCidade);
            ViewData["IdBairro"] = new SelectList(_context.Bairro.Where(b => b.IdCidade == pessoaDoadoraCadastroViewModel.IdCidade).OrderBy(b => b.Nome), "Id", "Nome", pessoaDoadoraCadastroViewModel.IdBairro);

            var listaTipoContato = Enum.GetValues(typeof(TipoFormaContato))
                .Cast<TipoFormaContato>()
                .Select(e => new { Valor = e, Nome = e.ToString() });

            ViewData["TiposFormaContato"] = new SelectList(listaTipoContato, "Valor", "Nome");

            var listaTipoPessoa = Enum.GetValues(typeof(TipoPessoaCadastro))
                .Cast<TipoPessoaCadastro>()
                .Select(e => new { Valor = e, Nome = e.ToString() });

            ViewData["TipoPessoaCadastro"] = new SelectList(listaTipoPessoa, "Valor", "Nome");

            return View(pessoaDoadoraCadastroViewModel);
        }



        // POST: PessoaDoadora/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PessoaDoadoraCadastroViewModel pessoaDoadoraCadastroViewModel)
        {
            bool isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            var pessoaDoadora = await _context.PessoaDoadora
                .Include(p => p.FormasContato)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (id != pessoaDoadoraCadastroViewModel.Id || (!User.IsInRole("ADMIN") && pessoaDoadora?.IdUsuario != User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                if (isAjax) return Json(new { sucesso = false, mensagem = "Acesso negado." });
                return NotFound();
            }

            ObrigatoriedadePessoaDoadora(pessoaDoadoraCadastroViewModel);
            if (ModelState.IsValid)
            {
                try
                {
                    if (pessoaDoadoraCadastroViewModel.TipoPessoaCadastro == TipoPessoaCadastro.Fisica)
                    {
                        var pessoaDoadoraFisica = (pessoaDoadora as PessoaFisica);
                        pessoaDoadoraFisica.CPF = pessoaDoadoraCadastroViewModel.CPF;
                        pessoaDoadoraFisica.DataNascimento = pessoaDoadoraCadastroViewModel.DataNascimento;
                        pessoaDoadora = pessoaDoadoraFisica;
                    }
                    else
                    {
                        var pessoaDoadoraJuridica = (pessoaDoadora as PessoaJuridica);
                        pessoaDoadoraJuridica.CNPJ = pessoaDoadoraCadastroViewModel.CNPJ;
                        pessoaDoadoraJuridica.Site = pessoaDoadoraCadastroViewModel.Site;
                        pessoaDoadoraJuridica.RedeSocial = pessoaDoadoraCadastroViewModel.Site;
                        pessoaDoadora = pessoaDoadoraJuridica;
                    }
                    pessoaDoadora.Nome = pessoaDoadoraCadastroViewModel.Nome;
                    var formascontatos = pessoaDoadora.FormasContato.ToList();
                    pessoaDoadora.FormasContato.Clear();
                    pessoaDoadora.FormasContato = pessoaDoadoraCadastroViewModel.TipoFormaContato.Select(t => new FormaContato { TipoFormaContato = t }).ToList();
                    pessoaDoadora.Telefone = pessoaDoadoraCadastroViewModel.Telefone;
                    pessoaDoadora.IdBairro = pessoaDoadoraCadastroViewModel.IdBairro;
                    pessoaDoadora.Id = pessoaDoadoraCadastroViewModel.Id;
                    _context.FormaContato.RemoveRange(formascontatos);
                    _context.Update(pessoaDoadora);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaDoadoraExists(pessoaDoadoraCadastroViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (pessoaDoadoraCadastroViewModel.TelaAdmin)
                {
                    TempData["Sucesso"] = "Dados alterados com sucesso.";
                    return RedirectToAction(nameof(Edit), new { idUsuario = pessoaDoadoraCadastroViewModel.IdUsuario });
                }
                if (isAjax)
                    return Json(new { sucesso = true, mensagem = "Dados alterados com sucesso." });
                return Redirect("/Identity/Account/Manage");
            }

            if (isAjax)
            {
                var erros = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { sucesso = false, mensagem = string.Join(" ", erros) });
            }

            ViewData["IdEstado"] = new SelectList(_context.Estado.OrderBy(e => e.Nome), "Id", "Nome");
            ViewData["IdCidade"] = new SelectList(new List<Cidade>());
            ViewData["IdBairro"] = new SelectList(new List<Bairro>());
            var listaTipoContato = Enum.GetValues(typeof(TipoFormaContato)).Cast<TipoFormaContato>().Select(e => new {
                Valor = e,
                Nome = e.ToString()
            });
            ViewData["TiposFormaContato"] = new SelectList(listaTipoContato, "Valor", "Nome");
            var listaTipoPessoa = Enum.GetValues(typeof(TipoPessoaCadastro)).Cast<TipoPessoaCadastro>().Select(e => new {
                Valor = e,
                Nome = e.ToString()
            });
            ViewData["TipoPessoaCadastro"] = new SelectList(listaTipoPessoa, "Valor", "Nome");
            return View(pessoaDoadoraCadastroViewModel);
        }

        // GET: PessoaDoadora/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaDoadora = await _context.PessoaDoadora
                .Include(p => p.Bairro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoaDoadora == null)
            {
                return NotFound();
            }

            // Busca o email pelo UserManager
            var usuario = await _userManager.FindByIdAsync(pessoaDoadora.IdUsuario);
            ViewData["Email"] = usuario?.Email;

            var animaisNaoAdotados = await _context.Animal
                .Include(a => a.TipoAnimal)
                .Include(a => a.Raca)
                .Where(a => a.IdPessoaDoadora == pessoaDoadora.Id && !a.Adotado)
                .ToListAsync();
            ViewData["AnimaisNaoAdotados"] = animaisNaoAdotados;

            return View(pessoaDoadora);
        }

        // POST: PessoaDoadora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoaDoadora = await _context.PessoaDoadora.FindAsync(id);
            if (pessoaDoadora != null)
            {
                pessoaDoadora.DataExclusao = DateTime.Now;

                var user = await _userManager.FindByIdAsync(pessoaDoadora.IdUsuario);
                await _userManager.SetLockoutEnabledAsync(user, true);
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);

                _context.Update(pessoaDoadora);

                var animaisNaoAdotados = await _context.Animal
                    .Where(a => a.IdPessoaDoadora == id && !a.Adotado)
                    .ToListAsync();

                foreach (var animal in animaisNaoAdotados)
                {
                    _context.RegistroCirurgia.RemoveRange(_context.RegistroCirurgia.Where(r => r.IdAnimal == animal.Id));
                    _context.RegistroVacina.RemoveRange(_context.RegistroVacina.Where(r => r.IdAnimal == animal.Id));
                    _context.Animal.Remove(animal);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RecuperarCidades(int? id)
        {
            return Json(new SelectList(await _context.Cidade.Where(c=>c.IdEstado==id).OrderBy(c => c.Nome).ToListAsync(), "Id", "Nome")); 
        }

        public async Task<IActionResult> RecuperarBairros(int? id)
        {
            return Json(new SelectList(await _context.Bairro.Where(c => c.IdCidade == id).OrderBy(b => b.Nome).ToListAsync(), "Id", "Nome"));
        }

        private void ObrigatoriedadePessoaDoadora(PessoaDoadoraCadastroViewModel pessoaDoadora)
        {
            if(pessoaDoadora.TipoPessoaCadastro == TipoPessoaCadastro.Fisica)
            {
                ModelState.Remove(nameof(PessoaDoadoraCadastroViewModel.Site));
                ModelState.Remove(nameof(PessoaDoadoraCadastroViewModel.CNPJ));
                ModelState.Remove(nameof(PessoaDoadoraCadastroViewModel.RedeSocial));
            }
            else
            {
                ModelState.Remove(nameof(PessoaDoadoraCadastroViewModel.CPF));
                ModelState.Remove(nameof(PessoaDoadoraCadastroViewModel.DataNascimento));
            }
        }

        public async Task<IActionResult> TornarAdmin(int id)
        {
            var pessoa = await _context.PessoaDoadora.FindAsync(id);
            if (pessoa == null) return NotFound();

            var usuario = await _userManager.FindByIdAsync(pessoa.IdUsuario);
            await _userManager.AddToRoleAsync(usuario, "ADMIN");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoverAdmin(int id)
        {
            var pessoa = await _context.PessoaDoadora.FindAsync(id);
            if (pessoa == null) return NotFound();

            var usuario = await _userManager.FindByIdAsync(pessoa.IdUsuario);
            await _userManager.RemoveFromRoleAsync(usuario, "ADMIN");

            return RedirectToAction(nameof(Index));
        }

        private bool PessoaDoadoraExists(int id)
        {
            return _context.PessoaDoadora.Any(e => e.Id == id);
        }

        [EnableQuery]
        public IQueryable<PessoaDoadora> Get()
        {
            return _context.PessoaDoadora;
        }
    }
}
