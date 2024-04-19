using Microsoft.AspNetCore.Mvc;

namespace PayAndPlay.Controllers
{
    // Controller responsável por efetuar o logout do utilizador, limpando as variáveis de sessão, e redirecionando para a página inicial
    public class LogOutController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetString("UTILIZADOR", "");
            HttpContext.Session.SetString("ID", "");
            HttpContext.Session.SetString("PERFIL", "");
            HttpContext.Session.SetString("ADMIN", "");
            TempData["Message"] = "Success: Logout efetuado com sucesso!";
            return RedirectToAction("Index", "Home");
        }
    }
}
