using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetDiverse.Data;

namespace PetDiverse.Controllers
{
    public class PessoaJuridicaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PessoaJuridicaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PessoaJuridicas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PessoaJuridica.Include(p => p.Bairro);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PessoaJuridicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaJuridica = await _context.PessoaJuridica
                .Include(p => p.Bairro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoaJuridica == null)
            {
                return NotFound();
            }

            return View(pessoaJuridica);
        }

        // GET: PessoaJuridicas/Create
        public IActionResult Create()
        {
            ViewData["IdBairro"] = new SelectList(_context.Bairro, "Id", "NomeBairro");
            return View();
        }

        // POST: PessoaJuridicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CNPJ,RedeSocial,Site,Id,Nome,Telefone,IdBairro")] PessoaJuridica pessoaJuridica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pessoaJuridica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBairro"] = new SelectList(_context.Bairro, "Id", "NomeBairro", pessoaJuridica.IdBairro);
            return View(pessoaJuridica);
        }

        // GET: PessoaJuridicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaJuridica = await _context.PessoaJuridica.FindAsync(id);
            if (pessoaJuridica == null)
            {
                return NotFound();
            }
            ViewData["IdBairro"] = new SelectList(_context.Bairro, "Id", "NomeBairro", pessoaJuridica.IdBairro);
            return View(pessoaJuridica);
        }

        // POST: PessoaJuridicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CNPJ,RedeSocial,Site,Id,Nome,Telefone,IdBairro")] PessoaJuridica pessoaJuridica)
        {
            if (id != pessoaJuridica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoaJuridica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaJuridicaExists(pessoaJuridica.Id))
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
            ViewData["IdBairro"] = new SelectList(_context.Bairro, "Id", "NomeBairro", pessoaJuridica.IdBairro);
            return View(pessoaJuridica);
        }

        // GET: PessoaJuridicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaJuridica = await _context.PessoaJuridica
                .Include(p => p.Bairro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoaJuridica == null)
            {
                return NotFound();
            }

            return View(pessoaJuridica);
        }

        // POST: PessoaJuridicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoaJuridica = await _context.PessoaJuridica.FindAsync(id);
            if (pessoaJuridica != null)
            {
                _context.PessoaJuridica.Remove(pessoaJuridica);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaJuridicaExists(int id)
        {
            return _context.PessoaJuridica.Any(e => e.Id == id);
        }
    }
}
