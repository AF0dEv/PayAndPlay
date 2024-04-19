using Microsoft.AspNetCore.Mvc;
using PayAndPlay.Data;
using PayAndPlay.Models;

namespace PayAndPlay.Controllers
{

    public class PerfilUserController : Controller
    {
        // Controlador para o perfil do utilizador, onde ele pode ver os seus gastos, apenas para utilizadores
        private readonly ApplicationDbContext _context;

        public PerfilUserController(ApplicationDbContext context)
        {
            _context = context;
        }
        private Listagem ls;

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "1" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                ls = new Listagem(_context);
                return View();
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        public IActionResult Index(int? rbtEscolha)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "1" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                ls = new Listagem(_context);
                if (rbtEscolha == null)
                {
                    ViewBag.RBTESCOLHA = rbtEscolha;
                    return View();
                }
                else if (rbtEscolha == 1)
                {
                    ViewBag.RBTESCOLHA = rbtEscolha;
                    ViewBag.MESES = true;
                    ViewBag.PERIODO = false;
                    return View();
                }
                else if (rbtEscolha == 2)
                {
                    ViewBag.RBTESCOLHA = rbtEscolha;
                    ViewBag.MESES = false;
                    ViewBag.PERIODO = true;
                    return View();
                }
                return View();
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult MostrarGastosMes(int? month)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "1" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                ls = new Listagem(_context);
                if (month != null)
                {
                    ViewBag.MONTH = month;
                    ViewBag.RBTESCOLHA = 1;
                    ViewBag.GASTOSMES = ls.ListarGastosMesPorDj(month + 1, int.Parse(HttpContext.Session.GetString("ID")));
                    ViewBag.MESES = true;
                    ViewBag.PERIODO = false;
                    return View("Index");
                }
                else
                {
                    return View("Index");
                }
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }

        }
        [HttpPost]
        public IActionResult MostrarGastosPeriodo(int? monthBegin, int? monthEnd)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "1" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                ls = new Listagem(_context);
                if (monthBegin <= monthEnd)
                {
                    if (monthBegin != null && monthEnd != null)
                    {
                        ViewBag.MONTHBEGIN = monthBegin;
                        ViewBag.MONTHEND = monthEnd;
                        ViewBag.RBTESCOLHA = 2;
                        ViewBag.GASTOSPERIODO = ls.ListarGastosPeriodo(monthBegin + 1, monthEnd + 1, int.Parse(HttpContext.Session.GetString("ID")));
                        ViewBag.MESES = false;
                        ViewBag.PERIODO = true;
                        return View("Index");
                    }
                    else
                    {
                        return View("Index");
                    }
                }
                else
                {
                    return View("Index");
                }
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
