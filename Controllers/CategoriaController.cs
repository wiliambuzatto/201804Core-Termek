using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Termek.Data;
using Termek.Models;

namespace Termek.Controllers
{
    public class CategoriaController : Controller
    {
        public readonly DataContext _context;
        public CategoriaController(DataContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            var categorias = _context.Categoria.ToList();
            ViewBag.Categorias = categorias;

            return View();
        }

        [HttpPost]
        public IActionResult Salvar(Categoria categoria)
        {
            _context.Categoria.Add(categoria);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}