using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PayAndPlay.Data;
using PayAndPlay.Models;

namespace PayAndPlay.Controllers
{
    // Estados possíveis de um pedido: Pendente, Concluido, Pago, Recusado
    public class PedidoDjController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidoDjController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var pedidosPagos = _context.Tpedidos.Include(p => p.Utilizador).Include(p => p.MusicaInPlayList.Musica).Include(p => p.MusicaInPlayList.PlayList).Include(p => p.DJ).Where(p => p.Estado == "PAGO");
            if (!pedidosPagos.IsNullOrEmpty())
            {
                ViewBag.PEDIDOSPAGOS = pedidosPagos;
            }
            else
            {
                ViewBag.PEDIDOSPAGOS = null;
            }
            var pedidosPendentes = _context.Tpedidos.Include(p => p.Utilizador).Include(p => p.MusicaInPlayList.Musica).Include(p => p.MusicaInPlayList.PlayList).Include(p => p.DJ).Where(p => p.Estado == "PENDENTE"); 
            if (!pedidosPendentes.IsNullOrEmpty())
            {
                ViewBag.PEDIDOSPENDENTES = pedidosPendentes;
            }
            else
            {
                ViewBag.PEDIDOSPENDENTES = null;
            }
            var pedidosConcluidos = _context.Tpedidos.Include(p => p.Utilizador).Include(p => p.MusicaInPlayList.Musica).Include(p => p.MusicaInPlayList.PlayList).Include(p => p.DJ).Where(p => p.Estado == "CONCLUIDO");
            if (!pedidosConcluidos.IsNullOrEmpty())
            {
                ViewBag.PEDIDOSCONCLUIDOS = pedidosConcluidos;
            }
            else
            {
                ViewBag.PEDIDOSCONCLUIDOS = null;
            }
            var pedidosRecusados = _context.Tpedidos.Include(p => p.Utilizador).Include(p => p.MusicaInPlayList.Musica).Include(p => p.MusicaInPlayList.PlayList).Include(p => p.DJ).Where(p => p.Estado == "RECUSADO");
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
        public IActionResult ConcluirPedido(int id)
        {
            var pedido = _context.Tpedidos.Find(id);
            pedido.Estado = "CONCLUIDO";
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult RecusarPedido(int id)
        {
            var pedido = _context.Tpedidos.Find(id);
            pedido.Estado = "RECUSADO";
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
