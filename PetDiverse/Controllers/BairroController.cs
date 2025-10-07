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
    public class BairroController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BairroController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bairro
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bairro.Include(b => b.Cidade);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bairro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bairro = await _context.Bairro
                .Include(b => b.Cidade)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bairro == null)
            {
                return NotFound();
            }

            return View(bairro);
        }

        // GET: Bairro/Create
        public IActionResult Create()
        {
            ViewData["IdCidade"] = new SelectList(_context.Cidade, "Id", "Nome");
            return View();
        }

        // POST: Bairro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,IdCidade")] Bairro bairro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bairro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCidade"] = new SelectList(_context.Cidade, "Id", "Nome", bairro.IdCidade);
            return View(bairro);
        }

        // GET: Bairro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bairro = await _context.Bairro.FindAsync(id);
            if (bairro == null)
            {
                return NotFound();
            }
            ViewData["IdCidade"] = new SelectList(_context.Cidade, "Id", "Nome", bairro.IdCidade);
            return View(bairro);
        }

        // POST: Bairro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,IdCidade")] Bairro bairro)
        {
            if (id != bairro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bairro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BairroExists(bairro.Id))
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
            ViewData["IdCidade"] = new SelectList(_context.Cidade, "Id", "Nome", bairro.IdCidade);
            return View(bairro);
        }

        // GET: Bairro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bairro = await _context.Bairro
                .Include(b => b.Cidade)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bairro == null)
            {
                return NotFound();
            }

            return View(bairro);
        }

        // POST: Bairro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bairro = await _context.Bairro.FindAsync(id);
            if (bairro != null)
            {
                _context.Bairro.Remove(bairro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BairroExists(int id)
        {
            return _context.Bairro.Any(e => e.Id == id);
        }
    }
}
