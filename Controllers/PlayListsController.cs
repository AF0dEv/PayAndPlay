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
    // CRUD de PlayLists para Admins
    public class PlayListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlayLists
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                var applicationDbContext = _context.TplayLists.Include(p => p.DJ);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: PlayLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {

                if (id == null)
                {
                    return NotFound();
                }

                var playList = await _context.TplayLists
                    .Include(p => p.DJ)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (playList == null)
                {
                    return NotFound();
                }
                return View(playList);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: PlayLists/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                ViewData["DJId"] = new SelectList(_context.Tdjs, "ID", "UserName");
                return View();
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: PlayLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,DJId")] PlayList playList)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (ModelState.IsValid)
                {
                    _context.Add(playList);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["DJId"] = new SelectList(_context.Tdjs, "ID", "UserName", playList.DJId);
                return View(playList);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: PlayLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {

                if (id == null)
                {
                    return NotFound();
                }

                var playList = await _context.TplayLists.FindAsync(id);
                if (playList == null)
                {
                    return NotFound();
                }
                ViewData["DJId"] = new SelectList(_context.Tdjs, "ID", "UserName", playList.DJId);
                return View(playList);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: PlayLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,DJId")] PlayList playList)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id != playList.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(playList);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PlayListExists(playList.ID))
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
                ViewData["DJId"] = new SelectList(_context.Tdjs, "ID", "UserName", playList.DJId);
                return View(playList);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: PlayLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {

                if (id == null)
                {
                    return NotFound();
                }

                var playList = await _context.TplayLists
                    .Include(p => p.DJ)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (playList == null)
                {
                    return NotFound();
                }

                return View(playList);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: PlayLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {

                var playList = await _context.TplayLists.FindAsync(id);
                if (playList != null)
                {
                    _context.TplayLists.Remove(playList);
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

        private bool PlayListExists(int id)
        {
            return _context.TplayLists.Any(e => e.ID == id);
        }
    }
}
