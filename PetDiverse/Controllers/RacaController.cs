using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetDiverse.Data;
using PetDiverse.Models;

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
        public async Task<IActionResult> Create(RacaViewModel racaViewModel)
        {
            if (ModelState.IsValid)
            {
                var raca = new Raca();
                raca.Id = racaViewModel.Id;
                raca.IdTipoAnimal = racaViewModel.IdTipoAnimal;
                raca.Descricao = racaViewModel.Descricao;
                _context.Add(raca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoAnimal"] = new SelectList(_context.TipoAnimal, "Id", "Descricao", racaViewModel.IdTipoAnimal);
            return View(racaViewModel);
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
            var racaViewModel = new RacaViewModel();
            racaViewModel.Id = raca.Id;
            racaViewModel.IdTipoAnimal = raca.IdTipoAnimal;
            racaViewModel.Descricao = raca.Descricao;
            ViewData["IdTipoAnimal"] = new SelectList(_context.TipoAnimal, "Id", "Descricao", raca.IdTipoAnimal);
            return View(racaViewModel);
        }

        // POST: Racas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RacaViewModel racaViewModel)
        {
            if (id != racaViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var raca = new Raca();
                    raca.Id = racaViewModel.Id;
                    raca.IdTipoAnimal = racaViewModel.IdTipoAnimal;
                    raca.Descricao = racaViewModel.Descricao;
                    _context.Update(raca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RacaExists(racaViewModel.Id))
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
            ViewData["IdTipoAnimal"] = new SelectList(_context.TipoAnimal, "Id", "Descricao", racaViewModel.IdTipoAnimal);
            return View(racaViewModel);
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
