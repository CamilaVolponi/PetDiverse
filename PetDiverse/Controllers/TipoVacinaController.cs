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
    public class TipoVacinaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoVacinaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoVacina
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TipoVacina.Include(t => t.TipoAnimal);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TipoVacina/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoVacina = await _context.TipoVacina
                .Include(t => t.TipoAnimal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoVacina == null)
            {
                return NotFound();
            }

            return View(tipoVacina);
        }

        // GET: TipoVacina/Create
        public IActionResult Create()
        {
            ViewData["IdTipoAnimal"] = new SelectList(_context.Set<TipoAnimal>(), "Id", "NomeTipoAnimal");
            return View();
        }

        // POST: TipoVacina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DescricaoVacina,IdTipoAnimal")] TipoVacina tipoVacina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoVacina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoAnimal"] = new SelectList(_context.Set<TipoAnimal>(), "Id", "NomeTipoAnimal", tipoVacina.IdTipoAnimal);
            return View(tipoVacina);
        }

        // GET: TipoVacina/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoVacina = await _context.TipoVacina.FindAsync(id);
            if (tipoVacina == null)
            {
                return NotFound();
            }
            ViewData["IdTipoAnimal"] = new SelectList(_context.Set<TipoAnimal>(), "Id", "NomeTipoAnimal", tipoVacina.IdTipoAnimal);
            return View(tipoVacina);
        }

        // POST: TipoVacina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DescricaoVacina,IdTipoAnimal")] TipoVacina tipoVacina)
        {
            if (id != tipoVacina.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoVacina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoVacinaExists(tipoVacina.Id))
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
            ViewData["IdTipoAnimal"] = new SelectList(_context.Set<TipoAnimal>(), "Id", "NomeTipoAnimal", tipoVacina.IdTipoAnimal);
            return View(tipoVacina);
        }

        // GET: TipoVacina/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoVacina = await _context.TipoVacina
                .Include(t => t.TipoAnimal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoVacina == null)
            {
                return NotFound();
            }

            return View(tipoVacina);
        }

        // POST: TipoVacina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoVacina = await _context.TipoVacina.FindAsync(id);
            if (tipoVacina != null)
            {
                _context.TipoVacina.Remove(tipoVacina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoVacinaExists(int id)
        {
            return _context.TipoVacina.Any(e => e.Id == id);
        }
    }
}
