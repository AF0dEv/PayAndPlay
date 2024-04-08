using Microsoft.AspNetCore.Mvc;

namespace PayAndPlay.Controllers
{
    public class LogOutController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetString("UTILIZADOR", "");

            return RedirectToAction("Index", "Login");
        }
    }
}
