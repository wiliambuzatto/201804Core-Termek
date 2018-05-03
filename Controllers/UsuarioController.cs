using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Termek.Data;
using Termek.Models;

namespace Termek.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly DataContext _context;
        public UsuarioController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var usuarios = _context.Usuario.ToList();
            return View(usuarios);
        }

        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            usuario.Senha = Criptografa(usuario.Senha);
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Excluir(int id)
        {
            var usuario = _context.Usuario.FirstOrDefault(u => u.Id == id);

            if(usuario == null)
                return RedirectToAction("Index");
               
            _context.Usuario.Remove(usuario);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Senha)
        {
            Senha = Criptografa(Senha);

            var usuario = _context.Usuario
                                  .Where(u => u.Email == Email && u.Senha == Senha)
                                  .FirstOrDefault();
            if(usuario != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.Perfil)  
                };

                var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return Redirect("/home/index");

            }

            return View();
        }




        public string Teste(string id)
        {
            return Criptografa(id);
        }

        [NonAction]
        private static string Criptografa(string senha)
        {
            var md5Hash = MD5.Create();

            // Converte a string para um vetor de byte e computa o Hash
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(senha));

            // Cria um StringBuilder para armazenar os bytes convertidos
            StringBuilder sBuilder = new StringBuilder();
            
            // Percorre cada byte do hash e formata cada um como uma string Hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}