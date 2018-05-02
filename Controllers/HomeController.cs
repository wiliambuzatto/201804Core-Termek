using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Termek.Data;
using Termek.Models;

namespace Termek.Controllers
{
    public class HomeController : Controller
    {

        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalProdutos = _context.Produto.Count();
            return View();
        }
    }
}
