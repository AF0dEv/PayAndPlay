using Microsoft.AspNetCore.Mvc;

namespace PayAndPlay.Controllers
{
    public class LogOutController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetString("UTILIZADOR", "");
            HttpContext.Session.SetString("ID", "");
            HttpContext.Session.SetString("PERFIL", "");
            HttpContext.Session.SetString("ADMIN", "");

            return RedirectToAction("Index", "Login");
        }
    }
}
