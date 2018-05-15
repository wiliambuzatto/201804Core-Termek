using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Termek.Data;

namespace Termek.Controllers
{
    public class SiteController : Controller
    {
        private readonly DataContext _context;

        public SiteController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var produtos = _context.Produto.ToList();
            return View(produtos);
        }

        [HttpGet]
        public IActionResult Buscar(string busca)
        {
            var produtos = _context.Produto.Where(p => 
                                                  p.Marca.Contains(busca) ||
                                                  p.Modelo.Contains(busca)
                                                  ).ToList();
            return View("Index", produtos);
        }
    }
}