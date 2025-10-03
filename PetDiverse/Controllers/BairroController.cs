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

        // GET: Bairroes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bairro.Include(b => b.Cidades);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bairroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bairro = await _context.Bairro
                .Include(b => b.Cidades)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bairro == null)
            {
                return NotFound();
            }

            return View(bairro);
        }

        // GET: Bairroes/Create
        public IActionResult Create()
        {
            ViewData["IdCidade"] = new SelectList(_context.Cidade, "Id", "NomeCidade");
            return View();
        }

        // POST: Bairroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeBairro,IdCidade")] Bairro bairro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bairro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCidade"] = new SelectList(_context.Cidade, "Id", "NomeCidade", bairro.IdCidade);
            return View(bairro);
        }

        // GET: Bairroes/Edit/5
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
            ViewData["IdCidade"] = new SelectList(_context.Cidade, "Id", "NomeCidade", bairro.IdCidade);
            return View(bairro);
        }

        // POST: Bairroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeBairro,IdCidade")] Bairro bairro)
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
            ViewData["IdCidade"] = new SelectList(_context.Cidade, "Id", "NomeCidade", bairro.IdCidade);
            return View(bairro);
        }

        // GET: Bairroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bairro = await _context.Bairro
                .Include(b => b.Cidades)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bairro == null)
            {
                return NotFound();
            }

            return View(bairro);
        }

        // POST: Bairroes/Delete/5
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
