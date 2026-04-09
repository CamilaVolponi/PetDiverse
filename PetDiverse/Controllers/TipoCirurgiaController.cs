using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class TipoCirurgiaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TipoCirurgiaController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Authorize(Roles = "ADMIN")]
        // GET: TipoCirurgia
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TipoCirurgia.Include(t => t.TipoAnimal);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "ADMIN")]
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

        [Authorize(Roles = "ADMIN")]
        // GET: TipoCirurgia/Create
        public IActionResult Create()
        {
            ViewData["IdTipoAnimal"] = new SelectList(_context.Set<TipoAnimal>(), "Id", "Descricao");
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        // POST: TipoCirurgia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TipoCirurgiaViewModel tipoCirurgiaViewModel)
        {
            if (ModelState.IsValid)
            {
                var tipoCirurgia = _mapper.Map<TipoCirurgia>(tipoCirurgiaViewModel);
                _context.Add(tipoCirurgia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoAnimal"] = new SelectList(_context.Set<TipoAnimal>(), "Id", "Descricao", tipoCirurgiaViewModel.IdTipoAnimal);
            return View(tipoCirurgiaViewModel);
        }

        [Authorize(Roles = "ADMIN")]
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

            var tipoCirurgiaViewModel = new TipoCirurgiaViewModel();
            tipoCirurgiaViewModel.Id = tipoCirurgia.Id;
            tipoCirurgiaViewModel.IdTipoAnimal = tipoCirurgia.IdTipoAnimal;
            tipoCirurgiaViewModel.Descricao = tipoCirurgia.Descricao;
            ViewData["IdTipoAnimal"] = new SelectList(_context.Set<TipoAnimal>(), "Id", "Descricao", tipoCirurgia.IdTipoAnimal);
            return View(tipoCirurgiaViewModel);
        }

        [Authorize(Roles = "ADMIN")]
        // POST: TipoCirurgia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TipoCirurgiaViewModel tipoCirurgiaViewModel)
        {
            if (id != tipoCirurgiaViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var tipoCirurgia = _mapper.Map<TipoCirurgia>(tipoCirurgiaViewModel);
                    _context.Update(tipoCirurgia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoCirurgiaExists(tipoCirurgiaViewModel.Id))
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
            ViewData["IdTipoAnimal"] = new SelectList(_context.Set<TipoAnimal>(), "Id", "Descricao", tipoCirurgiaViewModel.IdTipoAnimal);
            return View(tipoCirurgiaViewModel);
        }

        [Authorize(Roles = "ADMIN")]
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

        [Authorize(Roles = "ADMIN")]
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
