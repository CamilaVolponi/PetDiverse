using AutoMapper;
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
    public class RegistroCirurgiaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RegistroCirurgiaController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: RegistroCirurgia
        public async Task<IActionResult> Index(int idAnimal)
        {
            var applicationDbContext = _context.RegistroCirurgia.Where(r => r.IdAnimal == idAnimal);
            ViewData["NomeAnimal"] = _context.Animal.Find(idAnimal)?.Nome;
            ViewData["IdAnimal"] = idAnimal;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RegistroCirurgia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroCirurgia = await _context.RegistroCirurgia
                .Include(r => r.Animal)
                .Include(r => r.TipoCirurgia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroCirurgia == null)
            {
                return NotFound();
            }

            return View(registroCirurgia);
        }

        // GET: RegistroCirurgia/Create
        public IActionResult Create(int idAnimal)
        {
            var registroCirurgiaViewModel = new RegistroCirurgiaViewModel();
            var animal = _context.Animal.Find(idAnimal);
            registroCirurgiaViewModel.IdAnimal = idAnimal;
            ViewData["IdTipoCirurgia"] = new SelectList(_context.TipoCirurgia.Where(a => a.IdTipoAnimal == animal.IdTipoAnimal), "Id", "Descricao"); 
            return View(registroCirurgiaViewModel);
        }

        // POST: RegistroCirurgia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistroCirurgiaViewModel registroCirurgiaViewModel)
        {
            if (ModelState.IsValid)
            {
                var registroCirurgia = _mapper.Map<RegistroCirurgia>(registroCirurgiaViewModel);
                _context.Add(registroCirurgia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { registroCirurgia.IdAnimal });
            }
            var animal = _context.Animal.Find(registroCirurgiaViewModel.IdAnimal);
            ViewData["IdTipoCirurgia"] = new SelectList(_context.TipoCirurgia.Where(a => a.IdTipoAnimal == animal.IdTipoAnimal), "Id", "Descricao");
            return View(registroCirurgiaViewModel);
        }

        // GET: RegistroCirurgia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroCirurgia = await _context.RegistroCirurgia.FindAsync(id);
            if (registroCirurgia == null)
            {
                return NotFound();
            }
            var registroCirurgiaViewModel = _mapper.Map<RegistroCirurgiaViewModel>(registroCirurgia);
            var animal = _context.Animal.Find(registroCirurgiaViewModel.IdAnimal);
            ViewData["IdTipoCirurgia"] = new SelectList(_context.TipoCirurgia.Where(a => a.IdTipoAnimal == animal.IdTipoAnimal), "Id", "Descricao", registroCirurgiaViewModel.IdTipoCirurgia);
            return View(registroCirurgiaViewModel);
        }

        // POST: RegistroCirurgia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RegistroCirurgiaViewModel registroCirurgiaViewModel)
        {
            if (id != registroCirurgiaViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var registroCirurgia = _mapper.Map<RegistroCirurgia>(registroCirurgiaViewModel);
                    _context.Update(registroCirurgia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroCirurgiaExists(registroCirurgiaViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { registroCirurgiaViewModel.IdAnimal });
            }
            var animal = _context.Animal.Find(registroCirurgiaViewModel.IdAnimal);
            ViewData["IdTipoCirurgia"] = new SelectList(_context.TipoCirurgia.Where(a => a.IdTipoAnimal == animal.IdTipoAnimal), "Id", "Descricao", registroCirurgiaViewModel.IdTipoCirurgia);
            return View(registroCirurgiaViewModel);
        }

        // GET: RegistroCirurgia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroCirurgia = await _context.RegistroCirurgia
                .Include(r => r.Animal)
                .Include(r => r.TipoCirurgia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroCirurgia == null)
            {
                return NotFound();
            }

            return View(registroCirurgia);
        }

        // POST: RegistroCirurgia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int idAnimal)
        {
            var registroCirurgia = await _context.RegistroCirurgia.FindAsync(id);
            if (registroCirurgia != null)
            {
                _context.RegistroCirurgia.Remove(registroCirurgia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { idAnimal });
        }

        private bool RegistroCirurgiaExists(int id)
        {
            return _context.RegistroCirurgia.Any(e => e.Id == id);
        }
    }
}
