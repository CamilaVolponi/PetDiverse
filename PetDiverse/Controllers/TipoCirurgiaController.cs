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
    public class TipoCirurgiaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoCirurgiaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoCirurgia
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TipoCirurgia.Include(t => t.TipoAnimal);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TipoCirurgia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCirurgia = await _context.TipoCirurgia
                .Include(t => t.TipoAnimal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoCirurgia == null)
            {
                return NotFound();
            }

            return View(tipoCirurgia);
        }

        // GET: TipoCirurgia/Create
        public IActionResult Create()
        {
            ViewData["IdTipoAnimal"] = new SelectList(_context.Set<TipoAnimal>(), "Id", "NomeTipoAnimal");
            return View();
        }

        // POST: TipoCirurgia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DescricaoCirurgia,IdTipoAnimal")] TipoCirurgia tipoCirurgia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoCirurgia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoAnimal"] = new SelectList(_context.Set<TipoAnimal>(), "Id", "NomeTipoAnimal", tipoCirurgia.IdTipoAnimal);
            return View(tipoCirurgia);
        }

        // GET: TipoCirurgia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCirurgia = await _context.TipoCirurgia.FindAsync(id);
            if (tipoCirurgia == null)
            {
                return NotFound();
            }
            ViewData["IdTipoAnimal"] = new SelectList(_context.Set<TipoAnimal>(), "Id", "NomeTipoAnimal", tipoCirurgia.IdTipoAnimal);
            return View(tipoCirurgia);
        }

        // POST: TipoCirurgia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DescricaoCirurgia,IdTipoAnimal")] TipoCirurgia tipoCirurgia)
        {
            if (id != tipoCirurgia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoCirurgia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoCirurgiaExists(tipoCirurgia.Id))
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
            ViewData["IdTipoAnimal"] = new SelectList(_context.Set<TipoAnimal>(), "Id", "NomeTipoAnimal", tipoCirurgia.IdTipoAnimal);
            return View(tipoCirurgia);
        }

        // GET: TipoCirurgia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCirurgia = await _context.TipoCirurgia
                .Include(t => t.TipoAnimal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoCirurgia == null)
            {
                return NotFound();
            }

            return View(tipoCirurgia);
        }

        // POST: TipoCirurgia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoCirurgia = await _context.TipoCirurgia.FindAsync(id);
            if (tipoCirurgia != null)
            {
                _context.TipoCirurgia.Remove(tipoCirurgia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoCirurgiaExists(int id)
        {
            return _context.TipoCirurgia.Any(e => e.Id == id);
        }
    }
}
