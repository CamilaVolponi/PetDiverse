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
    public class BairroController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BairroController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
        public async Task<IActionResult> Create(BairroViewModel bairroViewModel)
        {
            if (ModelState.IsValid)
            {
                var bairro = _mapper.Map<Bairro>(bairroViewModel);
                bairro.Id = bairroViewModel.Id;
                bairro.Nome = bairroViewModel.Nome;
                bairro.IdCidade = bairroViewModel.IdCidade;
                _context.Add(bairro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCidade"] = new SelectList(_context.Cidade, "Id", "Nome", bairroViewModel.IdCidade);
            return View(bairroViewModel);
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
            var bairroViewModel = new BairroViewModel();
            bairroViewModel.Id = bairro.Id;
            bairroViewModel.Nome = bairro.Nome;
            bairroViewModel.IdCidade = bairro.IdCidade;
            ViewData["IdCidade"] = new SelectList(_context.Set<Cidade>(), "Id", "Nome", bairro.IdCidade);
            return View(bairroViewModel);
        }

        // POST: Bairro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BairroViewModel bairroViewModel)
        {
            if (id != bairroViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var bairro = _mapper.Map<Bairro>(bairroViewModel);
                    bairro.Id = bairroViewModel.Id;
                    bairro.Nome = bairroViewModel.Nome;
                    bairro.IdCidade = bairroViewModel.IdCidade;
                    _context.Update(bairro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BairroExists(bairroViewModel.Id))
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
            ViewData["IdCidade"] = new SelectList(_context.Cidade, "Id", "Nome", bairroViewModel.IdCidade);
            return View(bairroViewModel);
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
