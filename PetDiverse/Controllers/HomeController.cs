using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetDiverse.Data;
using PetDiverse.Models;
using System.Diagnostics;

namespace PetDiverse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index(int? idTipoAnimal, int? idEstado, int? idCidade, int? idBairro)
        {
            var animaisAdotados = _context.Animal.Where(a => !a.Adotado);

            if (idTipoAnimal != null)
            {
                animaisAdotados = animaisAdotados.Where(a => a.IdTipoAnimal == idTipoAnimal);
                ViewData["IdTipoAnimal"] = new SelectList(_context.TipoAnimal, "Id", "Descricao",idTipoAnimal);
            }
            else
            {
                ViewData["IdTipoAnimal"] = new SelectList(_context.TipoAnimal, "Id", "Descricao");
            }

            if (idEstado != null)
            {
                animaisAdotados = animaisAdotados.Where(a => a.PessoaDoadora.Bairro.Cidade.IdEstado == idEstado);
                ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Nome", idEstado);
            }
            else
            {
                ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Nome");
            }

            if (idCidade != null)
            {
                animaisAdotados = animaisAdotados.Where(a => a.PessoaDoadora.Bairro.IdCidade == idCidade);
                ViewData["IdCidade"] = new SelectList(_context.Cidade.Where(c => c.IdEstado == idEstado), "Id", "Nome", idCidade);
            }
            else if(idEstado != null)
            {
                ViewData["IdCidade"] = new SelectList(_context.Cidade.Where(c => c.IdEstado == idEstado), "Id", "Nome");
            } else
            {
                ViewData["IdCidade"] = new SelectList(new List<Cidade>(), "Id", "Nome");
            }

            if (idCidade != null)
            {
                animaisAdotados = animaisAdotados.Where(a => a.PessoaDoadora.IdBairro == idBairro);
                ViewData["IdBairro"] = new SelectList(_context.Bairro.Where(c => c.IdCidade == idCidade), "Id", "Nome", idBairro);
            }
            else if (idEstado != null)
            {
                ViewData["IdBairro"] = new SelectList(_context.Bairro.Where(c => c.IdCidade == idCidade), "Id", "Nome");
            }
            else
            {
                ViewData["IdBairro"] = new SelectList(new List<Bairro>(), "Id", "Nome");
            }

            return View(animaisAdotados);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> RecuperarCidades(int? id)
        {
            return Json(new SelectList(await _context.Cidade.Where(c => c.IdEstado == id).ToListAsync(), "Id", "Nome"));
        }

        public async Task<IActionResult> RecuperarBairros(int? id)
        {
            return Json(new SelectList(await _context.Bairro.Where(c => c.IdCidade == id).ToListAsync(), "Id", "Nome"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
