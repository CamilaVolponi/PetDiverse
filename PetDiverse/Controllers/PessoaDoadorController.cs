using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetDiverse.Data;
using PetDiverse.Data.Enums;
using PetDiverse.Models;
using System;
using System.Collections.Generic;
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
           //ViewData["TiposFormaContato"] = new SelectList(TipoFormaContato);
            return View();
        }

        // POST: PessoaDoadora/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PessoaDoadoraCadastroViewModel pessoaDoadoraCadastroViewModel)
        {
            if (ModelState.IsValid)
            {
                var pessoaDoadora = new PessoaDoadora();
                pessoaDoadora.Nome = pessoaDoadoraCadastroViewModel.Nome;
                pessoaDoadora.FormasContato = pessoaDoadoraCadastroViewModel.TipoFormaContato.Select(t => new FormaContato { TipoFormaContato = t });
                pessoaDoadora.Telefone = pessoaDoadoraCadastroViewModel.Telefone;
                pessoaDoadora.IdBairro = pessoaDoadoraCadastroViewModel.IdBairro;
                pessoaDoadora.IdUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if(pessoaDoadoraCadastroViewModel.TipoPessoaCadastro == TipoPessoaCadastro.Fisica)
                {
                    var pessoaDoadoraFisica = (pessoaDoadora as PessoaFisica);
                    pessoaDoadoraFisica.CPF = pessoaDoadoraCadastroViewModel.CPF;
                    pessoaDoadoraFisica.DataNascimento = pessoaDoadoraCadastroViewModel.DataNascimento;
                }
                else
                {
                    var pessoaDoadoraJuridica = (pessoaDoadora as PessoaJuridica);
                    pessoaDoadoraJuridica.CNPJ = pessoaDoadoraCadastroViewModel.CNPJ;
                    pessoaDoadoraJuridica.Site = pessoaDoadoraCadastroViewModel.Site;
                }
                _context.Add(pessoaDoadora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Nome");
            ViewData["IdCidade"] = new SelectList(new List<Cidade>());
            ViewData["IdBairro"] = new SelectList(new List<Bairro>());
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

            ViewData["IdBairro"] = new SelectList(_context.Bairro, "Id", "Nome", pessoaDoadora.IdBairro);
            return View(pessoaDoadora);
        }

        // POST: PessoaDoadora/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Telefone,IdBairro")] PessoaDoadora pessoaDoadora)
        {
            if (id != pessoaDoadora.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoaDoadora);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaDoadoraExists(pessoaDoadora.Id))
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
            ViewData["IdBairro"] = new SelectList(_context.Bairro, "Id", "Nome", pessoaDoadora.IdBairro);
            return View(pessoaDoadora);
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

        private bool PessoaDoadoraExists(int id)
        {
            return _context.PessoaDoadora.Any(e => e.Id == id);
        }
    }
}
