using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Termek.Data;
using Termek.Models;
using Termek.Models.ViewModels;

namespace Termek.Controllers
{
    [Authorize(Roles = "Administrador, Usuario")]
    public class ProdutoController : Controller
    {
        private readonly DataContext _context;
        private readonly IHostingEnvironment _env;

        public ProdutoController(DataContext context, IHostingEnvironment env)
        {
            _env = env;
            _context = context;
        }

        public IActionResult Index()
        {
            var produtos = _context.Produto.Include(p => p.Categoria).ToList();
            return View(produtos);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            var viewModel = new ProdutoViewModel();
            var categorias = _context.Categoria.ToList();
            viewModel.Categorias = categorias;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastrar(ProdutoViewModel model, IFormFile Foto)
        {

            var caminho = Path.Combine(_env.WebRootPath, "fotos");
            
            var nomeUnico = Guid.NewGuid().ToString();

            // Obter extensão
            var ext = Path.GetExtension(Foto.FileName);
            var nomeFoto = nomeUnico + ext;
            var caminhoCompleto = Path.Combine(caminho, nomeFoto);

            using(var fileStream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                Foto.CopyToAsync(fileStream);
            }

            var produto = new Produto();
            produto.Marca = model.Marca;
            produto.Modelo = model.Modelo;
            produto.Quantidade = model.Quantidade;
            produto.Valor = model.Valor;
            produto.Descricao = model.Descricao;
            produto.Foto = "fotos/" + nomeFoto;

            var categoria = _context.Categoria.FirstOrDefault(c => c.Id == model.CategoriaId);
            produto.Categoria = categoria;

            _context.Produto.Add(produto);
            _context.SaveChanges();

            model.Categorias = _context.Categoria.ToList();

            ViewBag.Mensagem = "Produto cadastrado com sucesso";

            return View(model);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var viewModel = new ProdutoViewModel();
            var prod = _context.Produto.Include(p => p.Categoria).FirstOrDefault(p => p.Id == id);
            viewModel.Id = prod.Id;
            viewModel.Marca = prod.Marca;
            viewModel.Modelo = prod.Modelo;
            viewModel.Valor = prod.Valor;
            viewModel.Quantidade = prod.Quantidade;
            viewModel.Descricao = prod.Descricao;
            viewModel.CategoriaId = prod.Categoria.Id;

            viewModel.Categorias = _context.Categoria.ToList();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Editar(ProdutoViewModel model)
        {
            var produto = _context.Produto.FirstOrDefault(p => p.Id == model.Id);
            produto.Marca = model.Marca;
            produto.Modelo = model.Modelo;
            produto.Quantidade = model.Quantidade;
            produto.Valor = model.Valor;
            produto.Descricao = model.Descricao;

            produto.Categoria = _context.Categoria.FirstOrDefault(c => c.Id == model.CategoriaId);

            _context.Produto.Update(produto);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Administrador, Usuario")]
        [HttpPost]
        public IActionResult Excluir(int id)
        {
            var produto = _context.Produto.FirstOrDefault(p => p.Id == id);
            _context.Produto.Remove(produto);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}