using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetDiverse.Data;
using PetDiverse.Data.Enums;
using PetDiverse.Data.Enuns;
using PetDiverse.Models;
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
        private readonly IMapper _mapper;

        public AnimalController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Animal
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("ADMIN"))
            {
                var todosAnimais = _context.Animal
                    .Include(a => a.TipoAnimal)
                    .Include(a => a.Raca);
                return View(await todosAnimais.ToListAsync());
            }

            var pessoaDoadora = await _context.PessoaDoadora.FirstAsync(p => p.IdUsuario == User.FindFirstValue(ClaimTypes.NameIdentifier));
            var applicationDbContext = _context.Animal.Where(a => a.IdPessoaDoadora == pessoaDoadora.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Animal/Details/5
        [AllowAnonymous]
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
            
            ViewData["IdTipoAnimal"] = new SelectList(_context.TipoAnimal, "Id", "Descricao");
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
            var listaSexoBiologico = Enum.GetValues(typeof(SexoBiologico)).Cast<SexoBiologico>().Select(e => new {
                Valor = e,
                Nome = e.ToString()
            });
            ViewData["SexoBiologico"] = new SelectList(listaSexoBiologico, "Valor", "Nome");
            ViewData["IdRaca"] = new SelectList(new List<Raca>());
            return View();
        }

        // POST: Animal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimalViewModel animalViewModel)
        {
            var pessoaDoadora = await _context.PessoaDoadora
                .FirstAsync(p => p.IdUsuario == User.FindFirstValue(ClaimTypes.NameIdentifier));
            var animal = _mapper.Map<AnimalViewModel, Animal>(animalViewModel);
            animal.IdPessoaDoadora = pessoaDoadora.Id;

            // ⚠️ Validação ANTES
            if (animalViewModel.Foto == null || animalViewModel.Foto.Length == 0)
            {
                ModelState.AddModelError("Foto", "Campo obrigatório!");
            }

            if (ModelState.IsValid)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(animalViewModel.Foto.FileName);
                var filePath = Path.Combine("wwwroot", "fotoPet", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await animalViewModel.Foto.CopyToAsync(stream);
                }

                animal.CaminhoFoto = $"fotoPet/{fileName}";
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopula selects se falhar a validação
            ViewData["IdTipoAnimal"] = new SelectList(_context.TipoAnimal, "Id", "Descricao", animalViewModel.IdTipoAnimal);
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
            var listaSexoBiologico = Enum.GetValues(typeof(SexoBiologico)).Cast<SexoBiologico>().Select(e => new {
                Valor = e,
                Nome = e.ToString()
            });
            ViewData["SexoBiologico"] = new SelectList(listaSexoBiologico, "Valor", "Nome");
            ViewData["IdRaca"] = new SelectList(new List<Raca>());

            return View(animalViewModel);
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
            var animalViewModel = new AnimalViewModel();
            ViewData["IdTipoAnimal"] = new SelectList(_context.TipoAnimal, "Id", "Descricao", animal.IdTipoAnimal);
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
            var listaSexoBiologico = Enum.GetValues(typeof(SexoBiologico)).Cast<SexoBiologico>().Select(e => new {
                Valor = e,
                Nome = e.ToString()
            });
            ViewData["SexoBiologico"] = new SelectList(listaSexoBiologico, "Valor", "Nome");
            ViewData["IdRaca"] = new SelectList(_context.Raca.Where(r => r.IdTipoAnimal == animal.IdTipoAnimal), "Id", "Descricao", animal.IdRaca);

            animalViewModel.Id = animal.Id;
            animalViewModel.Nome = animal.Nome;
            animalViewModel.Descricao = animal.Descricao;
            animalViewModel.Idade = animal.Idade;
            animalViewModel.ContagemIdade = animal.ContagemIdade;
            animalViewModel.Adotado = animal.Adotado;
            animalViewModel.CaminhoFoto = animal.CaminhoFoto;
            animalViewModel.SexoBiologico = animal.SexoBiologico;
            animalViewModel.Porte = animal.Porte;
            animalViewModel.IdTipoAnimal = animal.IdTipoAnimal;
            animalViewModel.IdRaca = animal.IdRaca;
            return View(animalViewModel);
        }

        // POST: Animal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AnimalViewModel animalViewModel)
        {
            if (id != animalViewModel.Id)
            {
                return NotFound();
            }

            var pessoaDoadora = await _context.PessoaDoadora.FirstAsync(p => p.IdUsuario==User.FindFirstValue(ClaimTypes.NameIdentifier));
            var animal = _mapper.Map<AnimalViewModel, Animal>(animalViewModel);
            animal.IdPessoaDoadora = pessoaDoadora.Id;
            if (ModelState.IsValid)
            {
                try
                {
                    // Verifica se foi enviada uma nova imagem
                    if (animalViewModel.Foto != null && animalViewModel.Foto.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(animalViewModel.Foto.FileName);
                        var filePath = Path.Combine("wwwroot\\fotoPet", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await animalViewModel.Foto.CopyToAsync(stream);
                        }

                        // Salva só o caminho relativo para exibir corretamente
                        animal.CaminhoFoto = $"fotoPet/{fileName}";
                    }
                    else
                    {
                        // Mantém a imagem antiga se não tiver nova
                        var animalExistente = await _context.Animal.AsNoTracking()
                            .FirstOrDefaultAsync(a => a.Id == animal.Id);

                        if (animalExistente != null)
                        {
                            animal.CaminhoFoto = animalExistente.CaminhoFoto;
                        }
                    }

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
            ViewData["IdTipoAnimal"] = new SelectList(_context.TipoAnimal, "Id", "Descricao", animalViewModel.IdTipoAnimal);
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
            var listaSexoBiologico = Enum.GetValues(typeof(SexoBiologico)).Cast<SexoBiologico>().Select(e => new {
                Valor = e,
                Nome = e.ToString()
            });
            ViewData["SexoBiologico"] = new SelectList(listaSexoBiologico, "Valor", "Nome");
            ViewData["IdRaca"] = new SelectList(_context.Raca.Where(r => r.IdTipoAnimal == animal.IdTipoAnimal), "Id", "Descricao", animal.IdRaca);
            return View(animalViewModel);
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
                var cirurgias = _context.RegistroCirurgia.Where(r => r.IdAnimal == id);
                _context.RegistroCirurgia.RemoveRange(cirurgias);

                var vacinas = _context.RegistroVacina.Where(r => r.IdAnimal == id);
                _context.RegistroVacina.RemoveRange(vacinas);

                _context.Animal.Remove(animal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Adotado(int id)
        {
            var animal = await _context.Animal.FindAsync(id);
            if (animal != null)
            {
                animal.Adotado = true;
                _context.Animal.Update(animal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RecuperarRaca(int? id)
        {
            return Json(new SelectList(await _context.Raca.Where(c => c.IdTipoAnimal == id).ToListAsync(), "Id", "Descricao"));
        }

        private bool AnimalExists(int id)
        {
            return _context.Animal.Any(e => e.Id == id);
        }
    }
}
