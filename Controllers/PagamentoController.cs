using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayAndPlay.Data;
using PayAndPlay.Models;

namespace PayAndPlay.Controllers
{
    public class PagamentoController : Controller
    {
        private IEnumerable<Pedido> pedido;
        private readonly ApplicationDbContext _context;
        public PagamentoController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id)
        {
            pedido = _context.Tpedidos.Include(p => p.DJ).Include(p => p.Utilizador).Include(p => p.MusicaInPlayList.PlayList).Include(p => p.MusicaInPlayList.Musica).Where(p => p.ID == id && p.Estado == "PENDENTE").ToList();
            return View(pedido);
        }
        public IActionResult PagarPedido(int id) 
        {
            Pedido p = _context.Tpedidos.Find(id);
            p.Estado = "PAGO";
            _context.SaveChanges();
            return RedirectToAction("Index", "PedidoUser");
        }
        public IActionResult RecusarPedido(int id) 
        {
            Pedido p = _context.Tpedidos.Find(id);
            p.Estado = "RECUSADO";
            _context.SaveChanges();
            return View();
        }
    }
}
