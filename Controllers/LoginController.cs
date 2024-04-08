using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using PayAndPlay.Data;
using PayAndPlay.Models;

namespace PayAndPlay.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginUtilizador(string email, string password)
        {
            Utilizador? user = _context.Tutilizadores.FirstOrDefault(u => u.Email == email && u.Password == password && u.confirmPassword == password);
            if (user == null)
            {
                return View();
            }
            else if (user.Is_Admin == true)
            {
                HttpContext.Session.SetString("UTILIZADOR", user.UserName!);
                HttpContext.Session.SetString("ID", user.ID.ToString()!);
                HttpContext.Session.SetString("PERFIL", "3");
                HttpContext.Session.SetString("ADMIN", "true");
                return Redirect("~/" + HttpContext.Session.GetString("CONTROLADOR") + "/Index");
            }
            else
            {
                HttpContext.Session.SetString("PERFIL", "1");
                HttpContext.Session.SetString("UTILIZADOR", user.UserName!);
                return Redirect("~/" + HttpContext.Session.GetString("CONTROLADOR") + "/Index");
            }
        }

        public IActionResult LoginDJ(string email, string password)
        {
            DJ? dj = _context.Tdjs.FirstOrDefault(u => u.Email == email && u.Password == password && u.confirmPassword == password);
            if (dj == null)
            {
                return View();
            }
            else
            {
                HttpContext.Session.SetString("ADMIN", "false");
                HttpContext.Session.SetString("PERFIL", "2");
                HttpContext.Session.SetString("UTILIZADOR", dj.UserName!);
                HttpContext.Session.SetString("ID", dj.ID.ToString()!);
                return Redirect("~/" + HttpContext.Session.GetString("CONTROLADOR") + "/Index");
            }
        }
    }
}
