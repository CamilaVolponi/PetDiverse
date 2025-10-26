using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetDiverse.Data;
using PetDiverse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetDiverse.Controllers
{
    public class RegistroVacinaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RegistroVacinaController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: RegistroVacina
        public async Task<IActionResult> Index(int idAnimal)
        {
            var applicationDbContext = _context.RegistroVacina.Where(r => r.IdAnimal == idAnimal);
            ViewData["NomeAnimal"] = _context.Animal.Find(idAnimal)?.Nome;
            ViewData["IdAnimal"] = idAnimal;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RegistroVacina/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroVacina = await _context.RegistroVacina
                .Include(r => r.Animal)
                .Include(r => r.TipoVacina)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroVacina == null)
            {
                return NotFound();
            }

            return View(registroVacina);
        }

        // GET: RegistroVacina/Create
        public IActionResult Create(int idAnimal)
        {
            var registroVacinaViewModel = new RegistroVacinaViewModel();
            var animal = _context.Animal.Find(idAnimal);
            registroVacinaViewModel.IdAnimal = idAnimal;
            ViewData["IdTipoVacina"] = new SelectList(_context.TipoVacina.Where(a => a.IdTipoAnimal == animal.IdTipoAnimal), "Id", "Descricao");
            return View(registroVacinaViewModel);
        }

        // POST: RegistroVacina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistroVacinaViewModel registroVacinaViewModel)
        {
            if (ModelState.IsValid)
            {
                var registroVacina = _mapper.Map<RegistroVacina>(registroVacinaViewModel);
                _context.Add(registroVacina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),new {registroVacina.IdAnimal });
            }
            var animal = _context.Animal.Find(registroVacinaViewModel.IdAnimal);
            ViewData["IdTipoVacina"] = new SelectList(_context.TipoVacina.Where(a => a.IdTipoAnimal == animal.IdTipoAnimal), "Id", "Descricao", registroVacinaViewModel.IdTipoVacina);
            return View(registroVacinaViewModel);
        }

        // GET: RegistroVacina/Edit/5
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
            var registroVacinaViewModel = _mapper.Map<RegistroVacinaViewModel>(registroVacina);
            var animal = _context.Animal.Find(registroVacinaViewModel.IdAnimal);
            ViewData["IdTipoVacina"] = new SelectList(_context.TipoVacina.Where(a => a.IdTipoAnimal == animal.IdTipoAnimal), "Id", "Descricao", registroVacina.IdTipoVacina);

            return View(registroVacinaViewModel);
        }

        // POST: RegistroVacina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RegistroVacinaViewModel registroVacinaViewModel)
        {
            if (id != registroVacinaViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var registroVacina = _mapper.Map<RegistroVacina>(registroVacinaViewModel);
                    _context.Update(registroVacina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroVacinaExists(registroVacinaViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { registroVacinaViewModel.IdAnimal });
            }
            var animal = _context.Animal.Find(registroVacinaViewModel.IdAnimal);
            ViewData["IdTipoVacina"] = new SelectList(_context.TipoVacina.Where(a => a.IdTipoAnimal == animal.IdTipoAnimal), "Id", "Descricao", registroVacinaViewModel.IdTipoVacina);
            return View(registroVacinaViewModel);
        }

        // GET: RegistroVacina/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroVacina = await _context.RegistroVacina
                .Include(r => r.Animal)
                .Include(r => r.TipoVacina)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroVacina == null)
            {
                return NotFound();
            }

            return View(registroVacina);
        }

        // POST: RegistroVacina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int idAnimal)
        {
            var registroVacina = await _context.RegistroVacina.FindAsync(id);
            if (registroVacina != null)
            {
                _context.RegistroVacina.Remove(registroVacina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { idAnimal });
        }

        private bool RegistroVacinaExists(int id)
        {
            return _context.RegistroVacina.Any(e => e.Id == id);
        }
    }
}
