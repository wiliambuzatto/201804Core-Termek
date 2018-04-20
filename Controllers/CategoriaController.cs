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

        public IActionResult Index(int id)
        {
            var categorias = _context.Categoria.ToList();
            ViewBag.Categorias = categorias;
            
            var categoria = _context.Categoria.FirstOrDefault(c => c.Id == id);
            
            return View(categoria);
        }

        [HttpPost]
        public IActionResult Salvar(Categoria categoria)
        {
            if(categoria.Id == 0)
            {
                _context.Categoria.Add(categoria);
            }
            else
            {
                var categoriaBanco = _context.Categoria.FirstOrDefault(c => c.Id == categoria.Id);
                categoriaBanco.Descricao = categoria.Descricao;
                _context.Categoria.Update(categoriaBanco);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Excluir(int id)
        {
    
            var categoria = _context.Categoria.FirstOrDefault(c => c.Id == id);
          
            _context.Categoria.Remove(categoria);
    
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}