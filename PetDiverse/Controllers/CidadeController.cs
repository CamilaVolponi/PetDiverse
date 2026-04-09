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
    public class CidadeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CidadeController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Authorize(Roles = "ADMIN")]
        // GET: Cidade
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Cidade.Include(c => c.Estado);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "ADMIN")]
        // GET: Cidade/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await _context.Cidade
                .Include(c => c.Estado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        [Authorize(Roles = "ADMIN")]
        // GET: Cidade/Create
        public IActionResult Create()
        {
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Nome");
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        // POST: Cidade/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CidadeViewModel cidadeViewModel)
        {
            if (ModelState.IsValid)
            {
                var cidade = _mapper.Map<Cidade>(cidadeViewModel);
                cidade.Id = cidadeViewModel.Id;
                cidade.Nome = cidadeViewModel.Nome;
                cidade.IdEstado = cidadeViewModel.IdEstado;
                _context.Add(cidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Nome", cidadeViewModel.IdEstado);
            return View(cidadeViewModel);
        }

        [Authorize(Roles = "ADMIN")]
        // GET: Cidade/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await _context.Cidade.FindAsync(id);
            if (cidade == null)
            {
                return NotFound();
            }
            var cidadeViewModel = new CidadeViewModel();
            cidadeViewModel.Id = cidade.Id;
            cidadeViewModel.Nome = cidade.Nome;
            cidadeViewModel.IdEstado = cidade.IdEstado;
            ViewData["IdEstado"] = new SelectList(_context.Set<Estado>(), "Id", "Nome", cidade.IdEstado);
            return View(cidadeViewModel);
        }

        [Authorize(Roles = "ADMIN")]
        // POST: Cidade/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CidadeViewModel cidadeViewModel)
        {
            if (id != cidadeViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var cidade = _mapper.Map<Cidade>(cidadeViewModel);
                    cidade.Id = cidadeViewModel.Id;
                    cidade.Nome = cidadeViewModel.Nome;
                    cidade.IdEstado = cidadeViewModel.IdEstado;
                    _context.Update(cidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CidadeExists(cidadeViewModel.Id))
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
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Nome", cidadeViewModel.IdEstado);
            return View(cidadeViewModel);
        }

        [Authorize(Roles = "ADMIN")]
        // GET: Cidade/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await _context.Cidade
                .Include(c => c.Estado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        [Authorize(Roles = "ADMIN")]
        // POST: Cidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cidade = await _context.Cidade.FindAsync(id);
            if (cidade != null)
            {
                _context.Cidade.Remove(cidade);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CidadeExists(int id)
        {
            return _context.Cidade.Any(e => e.Id == id);
        }
    }
}
