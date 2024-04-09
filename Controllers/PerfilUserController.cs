using Microsoft.AspNetCore.Mvc;
using PayAndPlay.Data;
using PayAndPlay.Models;

namespace PayAndPlay.Controllers
{

    public class PerfilUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerfilUserController(ApplicationDbContext context)
        {
            _context = context;
        }
        private Listagem ls;

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("ADMIN") == "false" && HttpContext.Session.GetString("PERFIL") == "1" && HttpContext.Session.GetString("UTILIZADOR") != "" || HttpContext.Session.GetString("UTILIZADOR") != null)
            {
                ls = new Listagem(_context);
                return View();
            }
            else
            {
                HttpContext.Session.SetString("CONTROLADOR", "PerfilDJ");
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        public IActionResult Index(int? rbtEscolha)
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

        public IActionResult MostrarGastosMes(int? month)
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
        [HttpPost]
        public IActionResult MostrarGastosPeriodo(int? monthBegin, int? monthEnd)
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
    }
}
