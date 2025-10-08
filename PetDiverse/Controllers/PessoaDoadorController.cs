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
    public class PessoaDoadorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PessoaDoadorController(ApplicationDbContext context)
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
            var listaTipoContato = Enum.GetValues(typeof(TipoFormaContato)).Cast<TipoFormaContato>().Select(e => new{ Valor = (int)e,
                Nome = e.ToString()});
            ViewData["TiposFormaContato"] = new SelectList(listaTipoContato, "Valor","Nome");
            var listaTipoPessoa = Enum.GetValues(typeof(TipoPessoaCadastro)).Cast<TipoPessoaCadastro>().Select(e => new {
                Valor = (int)e,
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Nome");
            ViewData["IdCidade"] = new SelectList(new List<Cidade>());
            ViewData["IdBairro"] = new SelectList(new List<Bairro>());
            var listaTipoContato = Enum.GetValues(typeof(TipoFormaContato)).Cast<TipoFormaContato>().Select(e => new {
                Valor = (int)e,
                Nome = e.ToString()
            });
            ViewData["TiposFormaContato"] = new SelectList(listaTipoContato, "Valor", "Nome");
            var listaTipoPessoa = Enum.GetValues(typeof(TipoPessoaCadastro)).Cast<TipoPessoaCadastro>().Select(e => new {
                Valor = (int)e,
                Nome = e.ToString()
            });
            ViewData["TipoPessoaCadastro"] = new SelectList(listaTipoPessoa, "Valor", "Nome");
            return View(pessoaDoadoraCadastroViewModel);
        }

        // GET: PessoaDoadora/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaDoadora = await _context.PessoaDoadora.FindAsync(id);
            if (pessoaDoadora == null)
            {
                return NotFound();
            }

            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Nome");
            ViewData["IdCidade"] = new SelectList(new List<Cidade>());
            ViewData["IdBairro"] = new SelectList(new List<Bairro>());
            var listaTipoContato = Enum.GetValues(typeof(TipoFormaContato)).Cast<TipoFormaContato>().Select(e => new {
                Valor = (int)e,
                Nome = e.ToString()
            });
            ViewData["TiposFormaContato"] = new SelectList(listaTipoContato, "Valor", "Nome");
            var listaTipoPessoa = Enum.GetValues(typeof(TipoPessoaCadastro)).Cast<TipoPessoaCadastro>().Select(e => new {
                Valor = (int)e,
                Nome = e.ToString()
            });
            ViewData["TipoPessoaCadastro"] = new SelectList(listaTipoPessoa, "Valor", "Nome");

            var pessoaDoadoraCadastroViewModel = new PessoaDoadoraCadastroViewModel();


            if (pessoaDoadora is PessoaFisica)
            {
                var pessoaDoadoraFisica = (pessoaDoadora as PessoaFisica);
                pessoaDoadoraCadastroViewModel.CPF = pessoaDoadoraFisica.CPF;
                pessoaDoadoraCadastroViewModel.DataNascimento = pessoaDoadoraFisica.DataNascimento;
            }
            else
            {
                var pessoaDoadoraJuridica = (pessoaDoadora as PessoaJuridica);
                pessoaDoadoraCadastroViewModel.CNPJ = pessoaDoadoraJuridica.CNPJ;
                pessoaDoadoraCadastroViewModel.Site = pessoaDoadoraJuridica.Site;
                pessoaDoadoraCadastroViewModel.RedeSocial = pessoaDoadoraJuridica.Site;
            }
            pessoaDoadoraCadastroViewModel.Nome =  pessoaDoadora.Nome;
            pessoaDoadoraCadastroViewModel.TipoFormaContato = pessoaDoadora.FormasContato.Select(t =>t.TipoFormaContato).ToList();
            pessoaDoadoraCadastroViewModel.Telefone = pessoaDoadora.Telefone;
            pessoaDoadoraCadastroViewModel.IdBairro = pessoaDoadora.IdBairro;

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
                    var pessoaDoadora = new PessoaDoadora();
                    if (pessoaDoadoraCadastroViewModel.TipoPessoaCadastro == TipoPessoaCadastro.Fisica)
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
                Valor = (int)e,
                Nome = e.ToString()
            });
            ViewData["TiposFormaContato"] = new SelectList(listaTipoContato, "Valor", "Nome");
            var listaTipoPessoa = Enum.GetValues(typeof(TipoPessoaCadastro)).Cast<TipoPessoaCadastro>().Select(e => new {
                Valor = (int)e,
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
