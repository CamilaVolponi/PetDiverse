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
    public class PessoaDoadorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PessoaDoadorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PessoaDoadoras
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PessoaDoadora.Include(p => p.Bairro);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PessoaDoadoras/Details/5
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

        // GET: PessoaDoadoras/Create
        public IActionResult Create()
        {
            ViewData["IdBairro"] = new SelectList(_context.Bairro, "Id", "NomeBairro");
            return View();
        }

        // POST: PessoaDoadoras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Telefone,IdBairro")] PessoaDoadora pessoaDoadora)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pessoaDoadora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBairro"] = new SelectList(_context.Bairro, "Id", "NomeBairro", pessoaDoadora.IdBairro);
            return View(pessoaDoadora);
        }

        // GET: PessoaDoadoras/Edit/5
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
            ViewData["IdBairro"] = new SelectList(_context.Bairro, "Id", "NomeBairro", pessoaDoadora.IdBairro);
            return View(pessoaDoadora);
        }

        // POST: PessoaDoadoras/Edit/5
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
            ViewData["IdBairro"] = new SelectList(_context.Bairro, "Id", "NomeBairro", pessoaDoadora.IdBairro);
            return View(pessoaDoadora);
        }

        // GET: PessoaDoadoras/Delete/5
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

        // POST: PessoaDoadoras/Delete/5
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

        private bool PessoaDoadoraExists(int id)
        {
            return _context.PessoaDoadora.Any(e => e.Id == id);
        }
    }
}
