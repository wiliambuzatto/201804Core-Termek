using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Termek.Data;
using Termek.Models;
using Termek.Models.ViewModels;

namespace Termek.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly DataContext _context;

        public ProdutoController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var produtos = _context.Produto.ToList();
            return View(produtos);
        }

        [HttpGet]
        public IActionResult Cadastrar(){
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(ProdutoViewModel model){
            var produto = new Produto();
            produto.Marca = model.Marca;
            produto.Modelo = model.Modelo;
            produto.Quantidade = model.Quantidade;
            produto.Valor = model.Valor;
            produto.Descricao = model.Descricao;

            var categoria = _context.Categoria.FirstOrDefault(c => c.Id == model.CategoriaId);
            produto.Categoria = categoria;

            _context.Produto.Add(produto);
            _context.SaveChanges();

            return View();
        }

        public IActionResult Excluir(int id)
        {
            var produto = _context.Produto.FirstOrDefault(p => p.Id == id);
            _context.Produto.Remove(produto);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}