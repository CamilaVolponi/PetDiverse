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
    public class RegistroVacinaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistroVacinaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RegistroVacinas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RegistroVacina.Include(r => r.Animal).Include(r => r.TiposVacina);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RegistroVacinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroVacina = await _context.RegistroVacina
                .Include(r => r.Animal)
                .Include(r => r.TiposVacina)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroVacina == null)
            {
                return NotFound();
            }

            return View(registroVacina);
        }

        // GET: RegistroVacinas/Create
        public IActionResult Create()
        {
            ViewData["IdAnimal"] = new SelectList(_context.Animal, "Id", "CaminhoFoto");
            ViewData["IdTipoVacina"] = new SelectList(_context.TipoVacina, "Id", "DescricaoVacina");
            return View();
        }

        // POST: RegistroVacinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataVacina,IdAnimal,IdTipoVacina")] RegistroVacina registroVacina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registroVacina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAnimal"] = new SelectList(_context.Animal, "Id", "CaminhoFoto", registroVacina.IdAnimal);
            ViewData["IdTipoVacina"] = new SelectList(_context.TipoVacina, "Id", "DescricaoVacina", registroVacina.IdTipoVacina);
            return View(registroVacina);
        }

        // GET: RegistroVacinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroVacina = await _context.RegistroVacina.FindAsync(id);
            if (registroVacina == null)
            {
                return NotFound();
            }
            ViewData["IdAnimal"] = new SelectList(_context.Animal, "Id", "CaminhoFoto", registroVacina.IdAnimal);
            ViewData["IdTipoVacina"] = new SelectList(_context.TipoVacina, "Id", "DescricaoVacina", registroVacina.IdTipoVacina);
            return View(registroVacina);
        }

        // POST: RegistroVacinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataVacina,IdAnimal,IdTipoVacina")] RegistroVacina registroVacina)
        {
            if (id != registroVacina.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registroVacina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroVacinaExists(registroVacina.Id))
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
            ViewData["IdAnimal"] = new SelectList(_context.Animal, "Id", "CaminhoFoto", registroVacina.IdAnimal);
            ViewData["IdTipoVacina"] = new SelectList(_context.TipoVacina, "Id", "DescricaoVacina", registroVacina.IdTipoVacina);
            return View(registroVacina);
        }

        // GET: RegistroVacinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroVacina = await _context.RegistroVacina
                .Include(r => r.Animal)
                .Include(r => r.TiposVacina)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroVacina == null)
            {
                return NotFound();
            }

            return View(registroVacina);
        }

        // POST: RegistroVacinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registroVacina = await _context.RegistroVacina.FindAsync(id);
            if (registroVacina != null)
            {
                _context.RegistroVacina.Remove(registroVacina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroVacinaExists(int id)
        {
            return _context.RegistroVacina.Any(e => e.Id == id);
        }
    }
}
