using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public PessoaDoadoraController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PessoaDoadora
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PessoaDoadora.Include(p => p.Bairro);
            return View(await applicationDbContext.ToListAsync());
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoaDoadora == null)
            {
                return NotFound();
            }

            return View(pessoaDoadora);
        }

        // GET: PessoaDoadora/Create
        public IActionResult Create()
        {
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Nome");
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
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Nome");
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

        // GET: PessoaDoadora/Edit/5
        // GET: PessoaDoadora/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var pessoaDoadora = await _context.PessoaDoadora
                .Include(p => p.Bairro)
                .ThenInclude(b => b.Cidade)
                .FirstOrDefaultAsync(p => p.IdUsuario == id);

            if (pessoaDoadora == null)
            {
                return NotFound();
            }

            var pessoaDoadoraCadastroViewModel = new PessoaDoadoraCadastroViewModel
            {
                Id = pessoaDoadora.Id,
                Nome = pessoaDoadora.Nome,
                IdBairro = pessoaDoadora.IdBairro,
                TipoFormaContato = pessoaDoadora.FormasContato.Select(fc => fc.TipoFormaContato).ToList(),
                Telefone = pessoaDoadora.Telefone,
                IdEstado = pessoaDoadora.Bairro.Cidade.IdEstado,
                IdCidade = pessoaDoadora.Bairro.IdCidade
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

            // ⚡️ Carrega os SelectLists já com os valores corretos selecionados
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Nome", pessoaDoadoraCadastroViewModel.IdEstado);
            ViewData["IdCidade"] = new SelectList(_context.Cidade.Where(c => c.IdEstado == pessoaDoadoraCadastroViewModel.IdEstado), "Id", "Nome", pessoaDoadoraCadastroViewModel.IdCidade);
            ViewData["IdBairro"] = new SelectList(_context.Bairro.Where(b => b.IdCidade == pessoaDoadoraCadastroViewModel.IdCidade), "Id", "Nome", pessoaDoadoraCadastroViewModel.IdBairro);

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
            if (id != pessoaDoadoraCadastroViewModel.Id)
            {
                return NotFound();
            }

            ObrigatoriedadePessoaDoadora(pessoaDoadoraCadastroViewModel);
            if (ModelState.IsValid)
            {
                try
                {
                    var pessoaDoadora = await _context.PessoaDoadora.FindAsync(id);
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Nome");
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
                _context.PessoaDoadora.Remove(pessoaDoadora);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RecuperarCidades(int? id)
        {
            return Json(new SelectList(await _context.Cidade.Where(c=>c.IdEstado==id).ToListAsync(), "Id", "Nome")); 
        }

        public async Task<IActionResult> RecuperarBairros(int? id)
        {
            return Json(new SelectList(await _context.Bairro.Where(c => c.IdCidade == id).ToListAsync(), "Id", "Nome"));
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

        private bool PessoaDoadoraExists(int id)
        {
            return _context.PessoaDoadora.Any(e => e.Id == id);
        }
    }
}
