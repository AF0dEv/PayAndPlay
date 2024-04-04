using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using PayAndPlay.Data;
using PayAndPlay.Models;

namespace PayAndPlay.Controllers
{
    public class RegistoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistoController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();  
        }


        public IActionResult RegistoUtilizador(string? email, string? username, string? password, string? confirmPassword)
        {
            if (email != null && username != null && password != null)
            {
                _context.Add(new Utilizador { Email = email, UserName = username, Password = password, confirmPassword = confirmPassword, Is_Admin = false, PerfilId = 1 });
                _context.SaveChanges();
                return Redirect("/Login");
            }
            else
            {
                return View();
            }
        }

        public IActionResult RegistoDJ(string? email, string? username, string? password, string? confirmPassword)
        {
            if (email != null && username != null && password != null)
            {
                _context.Add(new DJ { Email = email, UserName = username, Password = password, confirmPassword = confirmPassword, PerfilId = 2 });
                _context.SaveChanges();
                return Redirect("/Login");
            }
            else
            {
                return View();
            }
        }

    }
}
