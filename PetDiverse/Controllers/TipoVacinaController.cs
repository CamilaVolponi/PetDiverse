using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class TipoVacinaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TipoVacinaController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Authorize(Roles = "ADMIN")]
        // GET: TipoVacina
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TipoVacina.Include(t => t.TipoAnimal);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "ADMIN")]
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

        [Authorize(Roles = "ADMIN")]
        // GET: TipoVacina/Create
        public IActionResult Create()
        {
            ViewData["IdTipoAnimal"] = new SelectList(_context.Set<TipoAnimal>(), "Id", "Descricao");
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        // POST: TipoVacina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TipoVacinaViewModel tipoVacinaViewModel)
        {
            if (ModelState.IsValid)
            {
                var tipoVacina = _mapper.Map<TipoVacina>(tipoVacinaViewModel);
                _context.Add(tipoVacina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoAnimal"] = new SelectList(_context.Set<TipoAnimal>(), "Id", "Descricao", tipoVacinaViewModel.IdTipoAnimal);
            return View(tipoVacinaViewModel);
        }

        [Authorize(Roles = "ADMIN")]
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
            var tipoVacinaViewModel = new TipoVacinaViewModel();
            tipoVacinaViewModel.Id = tipoVacina.Id;
            tipoVacinaViewModel.IdTipoAnimal = tipoVacina.IdTipoAnimal;
            tipoVacinaViewModel.Descricao = tipoVacina.Descricao;
            ViewData["IdTipoAnimal"] = new SelectList(_context.Set<TipoAnimal>(), "Id", "Descricao", tipoVacina.IdTipoAnimal);
            return View(tipoVacinaViewModel);
        }

        [Authorize(Roles = "ADMIN")]
        // POST: TipoVacina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TipoVacinaViewModel tipoVacinaViewModel)
        {
            if (id != tipoVacinaViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var tipoVacina = _mapper.Map<TipoVacina>(tipoVacinaViewModel);
                    _context.Update(tipoVacina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoVacinaExists(tipoVacinaViewModel.Id))
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
            ViewData["IdTipoAnimal"] = new SelectList(_context.Set<TipoAnimal>(), "Id", "Descricao", tipoVacinaViewModel.IdTipoAnimal);
            return View(tipoVacinaViewModel);
        }

        [Authorize(Roles = "ADMIN")]
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

        [Authorize(Roles = "ADMIN")]
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
