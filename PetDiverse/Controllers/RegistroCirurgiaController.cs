using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetDiverse.Data;
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
        public IActionResult Create()
        {
            ViewData["IdAnimal"] = new SelectList(_context.Animal, "Id", "CaminhoFoto");
            ViewData["IdTipoCirurgia"] = new SelectList(_context.TipoCirurgia, "Id", "Descricao");
            return View();
        }

        // POST: RegistroCirurgia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataRegistro,IdAnimal,IdTipoCirurgia")] RegistroCirurgia registroCirurgia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registroCirurgia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAnimal"] = new SelectList(_context.Animal, "Id", "CaminhoFoto", registroCirurgia.IdAnimal);
            ViewData["IdTipoCirurgia"] = new SelectList(_context.TipoCirurgia, "Id", "Descricao", registroCirurgia.IdTipoCirurgia);
            return View(registroCirurgia);
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
            ViewData["IdAnimal"] = new SelectList(_context.Animal, "Id", "CaminhoFoto", registroCirurgia.IdAnimal);
            ViewData["IdTipoCirurgia"] = new SelectList(_context.TipoCirurgia, "Id", "Descricao", registroCirurgia.IdTipoCirurgia);
            return View(registroCirurgia);
        }

        // POST: RegistroCirurgia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataRegistro,IdAnimal,IdTipoCirurgia")] RegistroCirurgia registroCirurgia)
        {
            if (id != registroCirurgia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registroCirurgia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroCirurgiaExists(registroCirurgia.Id))
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
            ViewData["IdAnimal"] = new SelectList(_context.Animal, "Id", "CaminhoFoto", registroCirurgia.IdAnimal);
            ViewData["IdTipoCirurgia"] = new SelectList(_context.TipoCirurgia, "Id", "Descricao", registroCirurgia.IdTipoCirurgia);
            return View(registroCirurgia);
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registroCirurgia = await _context.RegistroCirurgia.FindAsync(id);
            if (registroCirurgia != null)
            {
                _context.RegistroCirurgia.Remove(registroCirurgia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroCirurgiaExists(int id)
        {
            return _context.RegistroCirurgia.Any(e => e.Id == id);
        }
    }
}
