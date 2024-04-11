using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayAndPlay.Data;

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
            ViewBag.PEDIDOSPENDENTES= _context.Tpedidos.Include(p => p.Utilizador).Include(p => p.DJ).Where(p => p.Estado == "Pendente");
            ViewBag.PEDIDOSCONCLUIDOS = _context.Tpedidos.Include(p => p.Utilizador).Include(p => p.DJ).Where(p => p.Estado == "Concluido");
            ViewBag.PEDIDOSRECUSADOS = _context.Tpedidos.Include(p => p.Utilizador).Include(p => p.DJ).Where(p => p.Estado == "Recusado");
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
