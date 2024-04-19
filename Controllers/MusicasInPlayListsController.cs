using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PayAndPlay.Data;
using PayAndPlay.Models;

namespace PayAndPlay.Controllers
{
    // CRUD MusicasInPlayLists para Admins
    public class MusicasInPlayListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MusicasInPlayListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MusicasInPlayLists
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                var applicationDbContext = _context.TmusicaInPlayLists.Include(m => m.Musica).Include(m => m.PlayList);
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
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var musicaInPlayList = await _context.TmusicaInPlayLists
                    .Include(m => m.Musica)
                    .Include(m => m.PlayList)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (musicaInPlayList == null)
                {
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

        // GET: MusicasInPlayLists/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                ViewData["MusicaId"] = new SelectList(_context.Tmusicas, "ID", "ID");
                ViewData["PlayListId"] = new SelectList(_context.TplayLists, "ID", "ID");
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
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (ModelState.IsValid)
                {
                    _context.Add(musicaInPlayList);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["MusicaId"] = new SelectList(_context.Tmusicas, "ID", "ID", musicaInPlayList.MusicaId);
                ViewData["PlayListId"] = new SelectList(_context.TplayLists, "ID", "ID", musicaInPlayList.PlayListId);
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
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var musicaInPlayList = await _context.TmusicaInPlayLists.FindAsync(id);
                if (musicaInPlayList == null)
                {
                    return NotFound();
                }
                ViewData["MusicaId"] = new SelectList(_context.Tmusicas, "ID", "ID", musicaInPlayList.MusicaId);
                ViewData["PlayListId"] = new SelectList(_context.TplayLists, "ID", "ID", musicaInPlayList.PlayListId);
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
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id != musicaInPlayList.ID)
                {
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
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["MusicaId"] = new SelectList(_context.Tmusicas, "ID", "ID", musicaInPlayList.MusicaId);
                ViewData["PlayListId"] = new SelectList(_context.TplayLists, "ID", "ID", musicaInPlayList.PlayListId);
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
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var musicaInPlayList = await _context.TmusicaInPlayLists
                    .Include(m => m.Musica)
                    .Include(m => m.PlayList)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (musicaInPlayList == null)
                {
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
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                var musicaInPlayList = await _context.TmusicaInPlayLists.FindAsync(id);
            if (musicaInPlayList != null)
            {
                _context.TmusicaInPlayLists.Remove(musicaInPlayList);
            }

            await _context.SaveChangesAsync();
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