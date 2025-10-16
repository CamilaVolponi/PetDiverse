using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetDiverse.Data;
using PetDiverse.Data.Enums;
using PetDiverse.Data.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetDiverse.Controllers
{
    public class AnimalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Animal
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Animal.Include(a => a.PessoaDoadora).Include(a => a.TipoAnimal);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Animal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal
                .Include(a => a.PessoaDoadora)
                .Include(a => a.TipoAnimal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animal/Create
        public IActionResult Create()
        {
            
            ViewData["IdTipoAnimal"] = new SelectList(_context.TipoAnimal, "Id", "NomeTipoAnimal");
            var listaContagemIdade = Enum.GetValues(typeof(ContagemIdade)).Cast<ContagemIdade>().Select(e => new {
                Valor = e,
                Nome = e.ToString()
            });
            ViewData["ContagemIdade"] = new SelectList(listaContagemIdade, "Valor", "Nome");
            var listaPorte = Enum.GetValues(typeof(PorteAnimal)).Cast<PorteAnimal>().Select(e => new {
                Valor = e,
                Nome = e.ToString()
            });
            ViewData["PorteAnimal"] = new SelectList(listaPorte, "Valor", "Nome");
            return View();
        }

        // POST: Animal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,Idade,ContagemIdade,Adotado,CaminhoFoto,Porte,IdTipoAnimal")] Animal animal)
        {
            var pessoaDoadora = await _context.PessoaDoadora.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            animal.IdPessoaDoadora = pessoaDoadora.Id;
            if (ModelState.IsValid)
            {
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["IdTipoAnimal"] = new SelectList(_context.TipoAnimal, "Id", "NomeTipoAnimal", animal.IdTipoAnimal);
            var listaContagemIdade = Enum.GetValues(typeof(ContagemIdade)).Cast<ContagemIdade>().Select(e => new {
                Valor = e,
                Nome = e.ToString()
            });
            ViewData["ContagemIdade"] = new SelectList(listaContagemIdade, "Valor", "Nome");
            var listaPorte = Enum.GetValues(typeof(PorteAnimal)).Cast<PorteAnimal>().Select(e => new {
                Valor = e,
                Nome = e.ToString()
            });
            ViewData["PorteAnimal"] = new SelectList(listaPorte, "Valor", "Nome");
            return View(animal);
        }

        // GET: Animal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            
            ViewData["IdTipoAnimal"] = new SelectList(_context.TipoAnimal, "Id", "NomeTipoAnimal", animal.IdTipoAnimal);
            var listaContagemIdade = Enum.GetValues(typeof(ContagemIdade)).Cast<ContagemIdade>().Select(e => new {
                Valor = e,
                Nome = e.ToString()
            });
            ViewData["ContagemIdade"] = new SelectList(listaContagemIdade, "Valor", "Nome");
            var listaPorte = Enum.GetValues(typeof(PorteAnimal)).Cast<PorteAnimal>().Select(e => new {
                Valor = e,
                Nome = e.ToString()
            });
            ViewData["PorteAnimal"] = new SelectList(listaPorte, "Valor", "Nome");
            return View(animal);
        }

        // POST: Animal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Idade,ContagemIdade,Adotado,CaminhoFoto,Porte,IdTipoAnimal")] Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }

            var pessoaDoadora = await _context.PessoaDoadora.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            animal.IdPessoaDoadora = pessoaDoadora.Id;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
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
            ViewData["IdTipoAnimal"] = new SelectList(_context.TipoAnimal, "Id", "NomeTipoAnimal", animal.IdTipoAnimal);
            var listaContagemIdade = Enum.GetValues(typeof(ContagemIdade)).Cast<ContagemIdade>().Select(e => new {
                Valor = e,
                Nome = e.ToString()
            });
            ViewData["ContagemIdade"] = new SelectList(listaContagemIdade, "Valor", "Nome");
            var listaPorte = Enum.GetValues(typeof(PorteAnimal)).Cast<PorteAnimal>().Select(e => new {
                Valor = e,
                Nome = e.ToString()
            });
            ViewData["PorteAnimal"] = new SelectList(listaPorte, "Valor", "Nome");
            return View(animal);
        }

        // GET: Animal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal
                .Include(a => a.PessoaDoadora)
                .Include(a => a.TipoAnimal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _context.Animal.FindAsync(id);
            if (animal != null)
            {
                _context.Animal.Remove(animal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return _context.Animal.Any(e => e.Id == id);
        }
    }
}
