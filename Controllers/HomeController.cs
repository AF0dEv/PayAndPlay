using Microsoft.AspNetCore.Mvc;
using PayAndPlay.Models;
using System.Diagnostics;

namespace PayAndPlay.Controllers
{
    // Landing Page, Onde o Utilizador Consegue Ver a P�gina Inicial, Caso N�o Esteja Logado ou seja Admin. 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UTILIZADOR") == "" || HttpContext.Session.GetString("UTILIZADOR") == null)
            {
                return View();
            }
            else if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "1" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                return RedirectToAction("Index", "PerfilUser");
            }
            else if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                return RedirectToAction("Index", "PerfilDJ");
            }
            else
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
