using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayAndPlay.Data;
using PayAndPlay.Models;

namespace PayAndPlay.Controllers
{
    // Estados possíveis de um pedido: Pendente, Concluido, Pago, Recusado
    // Controlador para a gestão de pagamentos dos Pedidos dos Utilizadores
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
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "1" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                pedido = _context.Tpedidos.Include(p => p.DJ).Include(p => p.Utilizador).Include(p => p.MusicaInPlayList.PlayList).Include(p => p.MusicaInPlayList.Musica).Where(p => p.ID == id && p.Estado == "PENDENTE").ToList();
                return View(pedido);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }

        }
        public IActionResult PagarPedido(int id) 
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "1" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                Pedido p = _context.Tpedidos.Find(id);
                p.Estado = "PAGO";
                _context.SaveChanges();
                TempData["Message"] = "Success: Pedido Pago";
                return RedirectToAction("Index", "PedidoUser");
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult RecusarPedido(int id) 
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "1" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                Pedido p = _context.Tpedidos.Find(id);
                p.Estado = "RECUSADO";
                _context.SaveChanges();
                TempData["Message"] = "Error: Pedido Recusado";
                return RedirectToAction("Index", "PedidoUser");
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
