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
    public class RacaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RacaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Racas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Raca.Include(r => r.TipoAnimal);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Racas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raca = await _context.Raca
                .Include(r => r.TipoAnimal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (raca == null)
            {
                return NotFound();
            }

            return View(raca);
        }

        // GET: Racas/Create
        public IActionResult Create()
        {
            ViewData["IdTipoAnimal"] = new SelectList(_context.TipoAnimal, "Id", "Descricao");
            return View();
        }

        // POST: Racas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,IdTipoAnimal")] Raca raca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(raca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoAnimal"] = new SelectList(_context.TipoAnimal, "Id", "Descricao", raca.IdTipoAnimal);
            return View(raca);
        }

        // GET: Racas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raca = await _context.Raca.FindAsync(id);
            if (raca == null)
            {
                return NotFound();
            }
            ViewData["IdTipoAnimal"] = new SelectList(_context.TipoAnimal, "Id", "Descricao", raca.IdTipoAnimal);
            return View(raca);
        }

        // POST: Racas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,IdTipoAnimal")] Raca raca)
        {
            if (id != raca.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(raca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RacaExists(raca.Id))
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
            ViewData["IdTipoAnimal"] = new SelectList(_context.TipoAnimal, "Id", "Descricao", raca.IdTipoAnimal);
            return View(raca);
        }

        // GET: Racas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raca = await _context.Raca
                .Include(r => r.TipoAnimal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (raca == null)
            {
                return NotFound();
            }

            return View(raca);
        }

        // POST: Racas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var raca = await _context.Raca.FindAsync(id);
            if (raca != null)
            {
                _context.Raca.Remove(raca);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RacaExists(int id)
        {
            return _context.Raca.Any(e => e.Id == id);
        }
    }
}
