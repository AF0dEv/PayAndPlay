using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PayAndPlay.Data;
using PayAndPlay.Models;

namespace PayAndPlay.Controllers
{
    public class PedidoUserController : Controller
    {
        private Pedido pedido;
        private readonly ApplicationDbContext _context;

        public PedidoUserController(ApplicationDbContext context)
        {
            pedido = new Pedido();
            _context = context;
        }
        public IActionResult Index(int? cbxDj)
        {
            if (cbxDj == null)
            {
                ViewBag.DJs = new SelectList(_context.Tdjs, "ID", "UserName");
                return View();
            }
            else
            {
                pedido.DJId = cbxDj ?? 0;
                pedido.UtilizadorId = int.Parse(HttpContext.Session.GetString("ID"));
                return RedirectToAction("PlayListDjPedidos", "PedidoUser", new { id = cbxDj });
            }
        }
        public IActionResult PlayListDjPedidos(int id)
        {
            ViewBag.PLAYLISTSDJ = _context.TplayLists.Where(p => p.DJId == id).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult PlayListDjPedidos(string id)
        {
            ViewBag.PLAYLISTSDJ = _context.TmusicaInPlayLists.Where(p => p.PlayListId == int.Parse(id));
            return RedirectToAction("MusicasDjPedidos", "PedidoUser", new { id = id });
        }
        public IActionResult MusicasDjPedidos(string id)
        {
            ViewBag.PLAYLISTS = _context.TplayLists.Find(int.Parse(id));
            ViewBag.MUSICAS = _context.Tmusicas.Where(m => m.MusicasInPlayLists.Any(mp => mp.PlayListId == int.Parse(id)));
            return View();
        }
        [HttpPost]
        public IActionResult MusicasDjPedidos(int id)
        {
            PlayList playlist= _context.TplayLists.Where(p => p.MusicasInPlayLists.Any(mp => mp.MusicaId == id)).FirstOrDefault();
            Musica musica = _context.Tmusicas.Find(id);
            MusicaInPlayList mip = _context.TmusicaInPlayLists.Where(p => p.MusicaId == musica.ID && p.PlayListId == playlist.ID).FirstOrDefault();
            pedido.MusicaInPlayListId = mip.ID;
            pedido.Estado = "PENDENTE";
            pedido.Data = DateOnly.Parse(DateTime.Now.ToString());
            pedido.Custo_Pedido = musica.Custo;
            return RedirectToAction("Index", "Pagamento", new { p = pedido });
            // Fiquei aqui, falta fazer a parte do pagamento, e depois de pagar, o pedido passa a concluido, testar este ultimo metodo antes de criar o pagamento
        }
    }
}
