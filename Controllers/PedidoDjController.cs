using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PayAndPlay.Data;
using PayAndPlay.Models;

namespace PayAndPlay.Controllers
{
    // Estados possíveis de um pedido: Pendente, Concluido, Pago, Recusado, Levantamento
    // Controlador dos Pedidos Recebidos
    public class PedidoDjController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidoDjController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                var pedidosPagos = _context.Tpedidos.Include(p => p.Utilizador).Include(p => p.MusicaInPlayList.Musica).Include(p => p.MusicaInPlayList.PlayList).Include(p => p.DJ).Where(p => p.Estado == "PAGO" && p.DJId == int.Parse(HttpContext.Session.GetString("ID")));
                if (!pedidosPagos.IsNullOrEmpty())
                {
                    ViewBag.PEDIDOSPAGOS = pedidosPagos;
                }
                else
                {
                    ViewBag.PEDIDOSPAGOS = null;
                }
                var pedidosPendentes = _context.Tpedidos.Include(p => p.Utilizador).Include(p => p.MusicaInPlayList.Musica).Include(p => p.MusicaInPlayList.PlayList).Include(p => p.DJ).Where(p => p.Estado == "PENDENTE" && p.DJId == int.Parse(HttpContext.Session.GetString("ID")));
                if (!pedidosPendentes.IsNullOrEmpty())
                {
                    ViewBag.PEDIDOSPENDENTES = pedidosPendentes;
                }
                else
                {
                    ViewBag.PEDIDOSPENDENTES = null;
                }
                var pedidosConcluidos = _context.Tpedidos.Include(p => p.Utilizador).Include(p => p.MusicaInPlayList.Musica).Include(p => p.MusicaInPlayList.PlayList).Include(p => p.DJ).Where(p => p.Estado == "CONCLUIDO" && p.DJId == int.Parse(HttpContext.Session.GetString("ID")));
                if (!pedidosConcluidos.IsNullOrEmpty())
                {
                    ViewBag.PEDIDOSCONCLUIDOS = pedidosConcluidos;
                }
                else
                {
                    ViewBag.PEDIDOSCONCLUIDOS = null;
                }
                var pedidosRecusados = _context.Tpedidos.Include(p => p.Utilizador).Include(p => p.MusicaInPlayList.Musica).Include(p => p.MusicaInPlayList.PlayList).Include(p => p.DJ).Where(p => p.Estado == "RECUSADO" && p.DJId == int.Parse(HttpContext.Session.GetString("ID")));
                if (!pedidosRecusados.IsNullOrEmpty())
                {
                    ViewBag.PEDIDOSRECUSADOS = pedidosRecusados;
                }
                else
                {
                    ViewBag.PEDIDOSRECUSADOS = null;
                }
                return View();
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }   
        public IActionResult ConcluirPedido(int id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                var pedido = _context.Tpedidos.Find(id);
                pedido.Estado = "CONCLUIDO";
                _context.SaveChanges();
                TempData["Message"] = "Success: Pedido Concluido com Sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult RecusarPedido(int id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                var pedido = _context.Tpedidos.Find(id);
                pedido.Estado = "RECUSADO";
                _context.SaveChanges();
                TempData["Message"] = "Error: Pedido Recusado!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
