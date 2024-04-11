using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PayAndPlay.Data;

namespace PayAndPlay.Controllers
{
    public class PedidoUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidoUserController(ApplicationDbContext context)
        {
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
                return RedirectToAction("PlayListDjPedidos", "PedidoUser", new { id = cbxDj });
            }
        }
        public IActionResult PlayListDjPedidos(string id)
        {
            ViewBag.PLAYLISTSDJ = _context.TplayLists.Where(p => p.DJId == int.Parse(id)).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult PlayListDjPedidos(int idPlayList)
        {
            ViewBag.PLAYLISTSDJ = _context.TmusicaInPlayLists.Where(p => p.PlayListId == idPlayList);
            return RedirectToAction("MusicasDjPedidos", "PedidoUser", new { id = idPlayList });
        }
        public IActionResult MusicasDjPedidos(string id)
        {
            ViewBag.MUSICASPLAYLISTDJ = _context.TmusicaInPlayLists.Where(p => p.PlayListId == int.Parse(id));
            return View();
        }
        [HttpPost]
        public IActionResult MusicasDjPedidos(int idMusica)
        {
            ViewBag.PLAYLISTSDJ = _context.TmusicaInPlayLists.Where(p => p.MusicaId == idMusica);
            return Redirect("PedidosUser");
        }
    }
}
