using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using PayAndPlay.Data;
using PayAndPlay.Models;

namespace PayAndPlay.Controllers
{
    // Controller para o registo de utilizadores e DJs , onde é verificado se o email ou username já existe na base de dados, caso exista é apresentada uma mensagem de erro, caso contrário é efetuado o registo com sucesso, sendo redirecionado para a página de login
    public class RegistoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistoController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HttpContext.Session.SetString("CONTROLADOR", "Registo");
            return View();  
        }


        public IActionResult RegistoUtilizador(string? email, string? username, string? password, string? confirmPassword)
        {
           
            if (email != null && username != null && password != null)
            {
                HttpContext.Session.SetString("CONTROLADOR", "Registo");
                Utilizador user = new Utilizador();
                user = _context.Tutilizadores.Where(u => u.Email == email || u.UserName == username).FirstOrDefault();
                if (user == null)
                {
                    _context.Add(new Utilizador { Email = email, UserName = username, Password = password, confirmPassword = confirmPassword, Is_Admin = false, PerfilId = 1 });
                    _context.SaveChanges();
                    TempData["Message"] = "Success: Registo Efetuado com Sucesso!";
                    return Redirect("/Login/LoginUtilizador");
                }
                else
                {
                    TempData["Message"] = "Error: Email ou Username Existente!";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public IActionResult RegistoDJ(string? email, string? username, string? password, string? confirmPassword)
        {
            HttpContext.Session.SetString("CONTROLADOR", "Registo");
            if (email != null && username != null && password != null)
            {
                DJ dj = new DJ();
                dj = _context.Tdjs.Where(d => d.Email == email || d.UserName == username).FirstOrDefault();
                if (dj == null) 
                { 
                    _context.Add(new DJ { Email = email, UserName = username, Password = password, confirmPassword = confirmPassword, PerfilId = 2 });
                    _context.SaveChanges();
                    TempData["Message"] = "Success: Registo efetuado com sucesso!";
                    return Redirect("/Login/LoginDJ");
                }
                else
                {
                    TempData["Message"] = "Error: Email ou Username Existente!";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

    }
}
