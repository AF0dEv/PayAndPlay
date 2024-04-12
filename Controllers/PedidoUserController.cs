using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PayAndPlay.Data;
using PayAndPlay.Models;

namespace PayAndPlay.Controllers
{
    public class PedidoUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidoUserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
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
            Pedido pedido = new Pedido();
            pedido.MusicaInPlayListId = mip.ID;
            pedido.DJId = playlist.DJId;
            pedido.UtilizadorId = int.Parse(HttpContext.Session.GetString("ID"));
            pedido.Estado = "PENDENTE";
            pedido.Data = DateOnly.FromDateTime(DateTime.Now);
            pedido.Custo_Pedido = musica.Custo;
            pedido.Utilizador = _context.Tutilizadores.Find(pedido.UtilizadorId);
            pedido.DJ = _context.Tdjs.Find(pedido.DJId);
            pedido.MusicaInPlayList = _context.TmusicaInPlayLists.Find(pedido.MusicaInPlayListId);
            _context.Tpedidos.Add(pedido);
            _context.SaveChanges();
            return RedirectToAction("Index", "Pagamento", new { id = pedido.ID });

            // Fiquei aqui, falta fazer a parte do pagamento, e depois de pagar, o pedido passa a concluido, testar este ultimo metodo antes de criar o pagamento
        }
    }
}
