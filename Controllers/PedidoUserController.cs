using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PayAndPlay.Data;
using PayAndPlay.Models;

namespace PayAndPlay.Controllers
{
    // Controller para a gestão de pedidos de utilizadores, onde estes podem escolher músicas de um DJ e fazer um pedido, que será posteriormente pago, para que o DJ toque a música escolhida, Acessivel apenas a utilizadores
    public class PedidoUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidoUserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "1" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                return View();
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult PlayListDjPedidos(int id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "1" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                ViewBag.PLAYLISTSDJ = _context.TplayLists.Where(p => p.DJId == id).ToList();
                return View();
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public IActionResult PlayListDjPedidos(string id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "1" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                ViewBag.PLAYLISTSDJ = _context.TmusicaInPlayLists.Where(p => p.PlayListId == int.Parse(id));
                return RedirectToAction("MusicasDjPedidos", "PedidoUser", new { id = id });
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult MusicasDjPedidos(string id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "1" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                ViewBag.PLAYLISTS = _context.TplayLists.Find(int.Parse(id));
                ViewBag.MUSICAS = _context.Tmusicas.Where(m => m.MusicasInPlayLists.Any(mp => mp.PlayListId == int.Parse(id)));
                return View();
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        public IActionResult MusicasDjPedidos(int id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "1" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                PlayList playlist = _context.TplayLists.Where(p => p.MusicasInPlayLists.Any(mp => mp.MusicaId == id)).FirstOrDefault();
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
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
