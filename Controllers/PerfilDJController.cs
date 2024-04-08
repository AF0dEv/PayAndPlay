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
        
        public IActionResult Index()
        {
            ls = new Listagem(_context);
            if (HttpContext.Session.GetString("ADMIN") == "false" && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("UTILIZADOR") != "" || HttpContext.Session.GetString("UTILIZADOR") != null)
            {  
                ViewBag.SALDO = ls.CalculoSaldoDJ(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.MUSICAMAISPEDIDAS = ls.ListarMusicasMaisPedidas(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.MUSICAMENOSPEDIDAS = ls.ListarMusicasMenosPedidas(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMAISPEDIDOS = ls.ListarUtilizadorMaisPedidos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMENOSPEDIDOS = ls.ListarUtilizadorMenosPedidos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMIASGASTOS = ls.ListarUtilizadorMaisGastos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMENOSGASTOS = ls.ListarUtilizadorMenosGastos(int.Parse(HttpContext.Session.GetString("ID")));
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
                ViewBag.SALDO = ls.CalculoSaldoDJ(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.MUSICAMAISPEDIDAS = ls.ListarMusicasMaisPedidas(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.MUSICAMENOSPEDIDAS = ls.ListarMusicasMenosPedidas(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMAISPEDIDOS = ls.ListarUtilizadorMaisPedidos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMENOSPEDIDOS = ls.ListarUtilizadorMenosPedidos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMIASGASTOS = ls.ListarUtilizadorMaisGastos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMENOSGASTOS = ls.ListarUtilizadorMenosGastos(int.Parse(HttpContext.Session.GetString("ID")));
                return View();
            }
            else if (rbtEscolha == 1)
            {
                ViewBag.SALDO = ls.CalculoSaldoDJ(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.MUSICAMAISPEDIDAS = ls.ListarMusicasMaisPedidas(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.MUSICAMENOSPEDIDAS = ls.ListarMusicasMenosPedidas(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMAISPEDIDOS = ls.ListarUtilizadorMaisPedidos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMENOSPEDIDOS = ls.ListarUtilizadorMenosPedidos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMAISGASTOS = ls.ListarUtilizadorMaisGastos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMENOSGASTOS = ls.ListarUtilizadorMenosGastos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.MESES = true;
                ViewBag.PERIODO = false;
                return View();
            }
            else if (rbtEscolha == 2)
            {
                ViewBag.SALDO = ls.CalculoSaldoDJ(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.MUSICAMAISPEDIDAS = ls.ListarMusicasMaisPedidas(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.MUSICAMENOSPEDIDAS = ls.ListarMusicasMenosPedidas(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMAISPEDIDOS = ls.ListarUtilizadorMaisPedidos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMENOSPEDIDOS = ls.ListarUtilizadorMenosPedidos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMIASGASTOS = ls.ListarUtilizadorMaisGastos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMENOSGASTOS = ls.ListarUtilizadorMenosGastos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.MESES = false;
                ViewBag.PERIODO = true;
            }
            return View();
        }
        [HttpPost]
        public IActionResult MostrarGanhosMesPeriodo(int? month, int? monthBegin, int? monthEnd)
        {
            ls = new Listagem(_context);
            if (month != null)
            {
                ViewBag.GANHOSMES = ls.ListarGanhosMes(month, int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.SALDO = ls.CalculoSaldoDJ(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.MUSICAMAISPEDIDAS = ls.ListarMusicasMaisPedidas(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.MUSICAMENOSPEDIDAS = ls.ListarMusicasMenosPedidas(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMAISPEDIDOS = ls.ListarUtilizadorMaisPedidos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMENOSPEDIDOS = ls.ListarUtilizadorMenosPedidos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMIASGASTOS = ls.ListarUtilizadorMaisGastos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMENOSGASTOS = ls.ListarUtilizadorMenosGastos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.MESES = true;
                ViewBag.PERIODO = false;
                return View();
            }
            else if(monthBegin != null && monthEnd != null)
            {
                ViewBag.GANHOSPERIODO = ls.ListarGanhosPeriodo(monthBegin, monthEnd, int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.SALDO = ls.CalculoSaldoDJ(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.MUSICAMAISPEDIDAS = ls.ListarMusicasMaisPedidas(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.MUSICAMENOSPEDIDAS = ls.ListarMusicasMenosPedidas(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMAISPEDIDOS = ls.ListarUtilizadorMaisPedidos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMENOSPEDIDOS = ls.ListarUtilizadorMenosPedidos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMIASGASTOS = ls.ListarUtilizadorMaisGastos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.UTILIZADORMENOSGASTOS = ls.ListarUtilizadorMenosGastos(int.Parse(HttpContext.Session.GetString("ID")));
                ViewBag.MESES = false;
                ViewBag.PERIODO = true;
                return View();
            }
            else
            {
                return View();
            }
        }
    }
}
