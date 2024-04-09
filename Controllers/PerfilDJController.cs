using Microsoft.AspNetCore.Mvc;
using PayAndPlay.Data;
using PayAndPlay.Models;

namespace PayAndPlay.Controllers
{
    public class PerfilDJController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public PerfilDJController(ApplicationDbContext context)
        {
            _context = context;
        }
        private Listagem ls;
        
        private void getViewBags()
        {
            ls = new Listagem(_context);
            ViewBag.SALDO = ls.CalculoSaldoDJ(int.Parse(HttpContext.Session.GetString("ID")));
            ViewBag.MUSICAMAISPEDIDAS = ls.ListarMusicasMaisPedidas(int.Parse(HttpContext.Session.GetString("ID")));
            ViewBag.MUSICAMENOSPEDIDAS = ls.ListarMusicasMenosPedidas(int.Parse(HttpContext.Session.GetString("ID")));
            ViewBag.UTILIZADORMAISPEDIDOS = ls.ListarUtilizadorMaisPedidos(int.Parse(HttpContext.Session.GetString("ID")));
            ViewBag.UTILIZADORMENOSPEDIDOS = ls.ListarUtilizadorMenosPedidos(int.Parse(HttpContext.Session.GetString("ID")));
            ViewBag.UTILIZADORMAISGASTOS = ls.ListarUtilizadorMaisGastos(int.Parse(HttpContext.Session.GetString("ID")));
            ViewBag.UTILIZADORMENOSGASTOS = ls.ListarUtilizadorMenosGastos(int.Parse(HttpContext.Session.GetString("ID")));
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("ADMIN") == "false" && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("UTILIZADOR") != "" || HttpContext.Session.GetString("UTILIZADOR") != null)
            {  
                getViewBags();
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
                getViewBags();
                return View();
            }
            else if (rbtEscolha == 1)
            {
                ViewBag.RBTESCOLHA = rbtEscolha;
                getViewBags();
                ViewBag.MESES = true;
                ViewBag.PERIODO = false;
                return View();
            }
            else if (rbtEscolha == 2)
            {
                ViewBag.RBTESCOLHA = rbtEscolha;
                getViewBags();
                ViewBag.MESES = false;
                ViewBag.PERIODO = true;
            }
            return View();
        }
        [HttpPost]
        public IActionResult MostrarGanhosMes(int? month)
        {
            ls = new Listagem(_context);
            if (month != null)
            {
                ViewBag.MONTH = month;
                ViewBag.RBTESCOLHA = 1;
                ViewBag.GANHOSMES = ls.ListarGanhosMes(month +1, int.Parse(HttpContext.Session.GetString("ID")));
                getViewBags();
                ViewBag.MESES = true;
                ViewBag.PERIODO = false;
                return View("Index");
            }else
            {
                return View("Index");
            }
            
        }
        [HttpPost]
        public IActionResult MostrarGanhosPeriodo(int? monthBegin, int? monthEnd)
        {
            ls = new Listagem(_context);
            if (monthBegin < monthEnd)
            {
                if (monthBegin != null && monthEnd != null)
                {
                    ViewBag.MONTHBEGIN = monthBegin;
                    ViewBag.MONTHEND = monthEnd;
                    ViewBag.RBTESCOLHA = 2;
                    var ganhosPorPeriodo = ls.ListarGanhosPeriodo(monthBegin + 1, monthEnd + 1, int.Parse(HttpContext.Session.GetString("ID")));
                    if (ganhosPorPeriodo == null)
                    {
                        return View("Index");
                    }else
                    {
                        ViewBag.GANHOSPERIODO = ganhosPorPeriodo;
                        getViewBags();
                        ViewBag.MESES = false;
                        ViewBag.PERIODO = true;
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
                return View("Index");
            }
        }
    }
}
