using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PayAndPlay.Data;
using PayAndPlay.Models;

namespace PayAndPlay.Controllers
{
    // CRUD Criado para o DJ gerenciar suas PlayLists, onde ele pode adicionar, editar e remover PlayLists, MVC Controller
    public class DjPlayListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DjPlayListController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlayLists
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                var applicationDbContext = _context.TplayLists.Where(p => p.DJId == int.Parse(HttpContext.Session.GetString("ID"))).Include(p => p.DJ);
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
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                if (id == null)
                {
                    TempData["Message"] = "Error: PlayList não encontrada!";
                    return NotFound();
                }

                var playList = await _context.TplayLists
                    .Include(p => p.DJ)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (playList == null)
                {
                    TempData["Message"] = "Error: PlayList não encontrada!";
                    return NotFound();
                }

                TempData["Message"] = "Success: PlayList encontrada!";
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
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
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
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                if (ModelState.IsValid)
                {
                    _context.Add(playList);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Success: PlayList criada com sucesso!";
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
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                if (id == null)
                {
                    TempData["Message"] = "Error: PlayList não encontrada!";
                    return NotFound();
                }

                var playList = await _context.TplayLists.FindAsync(id);
                if (playList == null)
                {
                    TempData["Message"] = "Error: PlayList não encontrada!";
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
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                if (id != playList.ID)
                {
                    TempData["Message"] = "Error: PlayList não encontrada!";
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
                            TempData["Message"] = "Error: PlayList não encontrada!";
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    TempData["Message"] = "Success: PlayList atualizada com sucesso!";
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
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                if (id == null)
                {
                    TempData["Message"] = "Error: PlayList não encontrada!";
                    return NotFound();
                }

                var playList = await _context.TplayLists
                    .Include(p => p.DJ)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (playList == null)
                {
                    TempData["Message"] = "Error: PlayList não encontrada!";
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
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("PERFIL") == "2" && HttpContext.Session.GetString("ADMIN") == "false")
            {
                var playList = await _context.TplayLists.FindAsync(id);
                if (playList != null)
                {
                    _context.TplayLists.Remove(playList);
                }

                await _context.SaveChangesAsync();
                TempData["Message"] = "Success: PlayList removida com sucesso!";
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
