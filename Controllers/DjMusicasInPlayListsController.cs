using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PayAndPlay.Data;
using PayAndPlay.Models;
using System.Linq;

namespace PayAndPlay.Controllers
{
    // CRUD Criado para o DJ gerenciar as musicas em suas PlayLists, onde ele pode adicionar, editar e remover musicas de suas PlayLists, MVC Controller
    public class DjMusicasInPlayListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DjMusicasInPlayListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MusicasInPlayLists
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                var applicationDbContext = _context.TmusicaInPlayLists.Include(m => m.Musica).Include(m => m.PlayList).Where(p => p.PlayList.DJId == int.Parse(HttpContext.Session.GetString("ID")));
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: MusicasInPlayLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                if (id == null)
                {
                    TempData["Message"] = "Error: Musicas na PlayList não encontrada!";
                    return NotFound();
                }

                var musicaInPlayList = await _context.TmusicaInPlayLists
                    .Include(m => m.Musica)
                    .Include(m => m.PlayList)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (musicaInPlayList == null)
                {
                    TempData["Message"] = "Error: Musicas na PlayList não encontrada!";
                    return NotFound();
                }

                TempData["Message"] = "Success: Musicas na PlayList encontrados!";
                return View(musicaInPlayList);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: MusicasInPlayLists/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                ViewData["MusicaId"] = new SelectList(_context.Tmusicas, "ID", "Nome");
                ViewData["PlayListId"] = new SelectList(_context.TplayLists.Where(p => p.DJId == int.Parse(HttpContext.Session.GetString("ID"))), "ID", "Nome");
                return View();
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: MusicasInPlayLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MusicaId,PlayListId")] MusicaInPlayList musicaInPlayList)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                if (ModelState.IsValid)
                {
                    _context.Add(musicaInPlayList);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Success: Musica adicionada na PlayList com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                ViewData["MusicaId"] = new SelectList(_context.Tmusicas, "ID", "Nome", musicaInPlayList.MusicaId);
                ViewData["PlayListId"] = new SelectList(_context.TplayLists.Where(p => p.DJId == int.Parse(HttpContext.Session.GetString("ID"))), "ID", "Nome", musicaInPlayList.PlayListId);
                return View(musicaInPlayList);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: MusicasInPlayLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                if (id == null)
                {
                    TempData["Message"] = "Error: Musica na PlayList não encontrada!";
                    return NotFound();
                }

                var musicaInPlayList = await _context.TmusicaInPlayLists.FindAsync(id);
                if (musicaInPlayList == null)
                {
                    TempData["Message"] = "Error: Musica na PlayList não encontrada!";
                    return NotFound();
                }
                ViewData["MusicaId"] = new SelectList(_context.Tmusicas, "ID", "Nome");
                ViewData["PlayListId"] = new SelectList(_context.TplayLists.Where(p => p.DJId == int.Parse(HttpContext.Session.GetString("ID"))), "ID", "Nome");
                return View(musicaInPlayList);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: MusicasInPlayLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MusicaId,PlayListId")] MusicaInPlayList musicaInPlayList)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                if (id != musicaInPlayList.ID)
                {
                    TempData["Message"] = "Error: Musica na PlayList não encontrada!";
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(musicaInPlayList);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MusicaInPlayListExists(musicaInPlayList.ID))
                        {
                            TempData["Message"] = "Error: Musica na PlayList não encontrada!";
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    TempData["Message"] = "Success: Musica na PlayList atualizada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                ViewData["MusicaId"] = new SelectList(_context.Tmusicas, "ID", "Nome");
                ViewData["PlayListId"] = new SelectList(_context.TplayLists.Where(p => p.DJId == int.Parse(HttpContext.Session.GetString("ID"))), "ID", "Nome");
                return View(musicaInPlayList);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: MusicasInPlayLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                if (id == null)
                {
                    TempData["Message"] = "Error: Musica na PlayList não encontrada!";
                    return NotFound();
                }

                var musicaInPlayList = await _context.TmusicaInPlayLists
                    .Include(m => m.Musica)
                    .Include(m => m.PlayList)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (musicaInPlayList == null)
                {
                    TempData["Message"] = "Error: Musica na PlayList não encontrada!";
                    return NotFound();
                }

                return View(musicaInPlayList);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: MusicasInPlayLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                var musicaInPlayList = await _context.TmusicaInPlayLists.FindAsync(id);
                if (musicaInPlayList != null)
                {
                    _context.TmusicaInPlayLists.Remove(musicaInPlayList);
                }
                try
                {
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Success: Musica removida da PlayList com sucesso!";
                }
                catch (DbUpdateException)
                {
                    TempData["Message"] = "Error: Esta Musica nao pode ser removida da Playlist! Por Favor, Contacte Administrador!";
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        private bool MusicaInPlayListExists(int id)
        {
            return _context.TmusicaInPlayLists.Any(e => e.ID == id);
        }
    }
}

