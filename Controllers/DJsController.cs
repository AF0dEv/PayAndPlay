using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PayAndPlay.Data;
using PayAndPlay.Models;

namespace PayAndPlay.Controllers
{
    public class DJsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DJsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DJs
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("ADMIN") == "" || HttpContext.Session.GetString("ADMIN") == null && HttpContext.Session.GetString("UTILIZADOR") == "" || HttpContext.Session.GetString("UTILIZADOR") == null)
            {
                HttpContext.Session.SetString("CONTROLADOR", "DJs");
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var applicationDbContext = _context.Tdjs.Include(d => d.Perfil);
                return View(await applicationDbContext.ToListAsync());
            }
        }

        // GET: DJs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("ADMIN") == "" || HttpContext.Session.GetString("ADMIN") == null && HttpContext.Session.GetString("UTILIZADOR") == "" || HttpContext.Session.GetString("UTILIZADOR") == null)
            {
                HttpContext.Session.SetString("CONTROLADOR", "DJs/Details");
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var dJ = await _context.Tdjs
                    .Include(d => d.Perfil)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (dJ == null)
                {
                    return NotFound();
                }

                return View(dJ);
            }
        }

        // GET: DJs/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("ADMIN") == "" || HttpContext.Session.GetString("ADMIN") == null && HttpContext.Session.GetString("UTILIZADOR") == "" || HttpContext.Session.GetString("UTILIZADOR") == null)
            {
                HttpContext.Session.SetString("CONTROLADOR", "DJs/Create");
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewData["PerfilId"] = new SelectList(_context.Tperfis, "ID", "ID");
                return View();
            }
        }

        // POST: DJs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Email,UserName,Password,confirmPassword,PerfilId")] DJ dJ)
        {
            if (HttpContext.Session.GetString("ADMIN") == "" || HttpContext.Session.GetString("ADMIN") == null && HttpContext.Session.GetString("UTILIZADOR") == "" || HttpContext.Session.GetString("UTILIZADOR") == null)
            {
                HttpContext.Session.SetString("CONTROLADOR", "DJs/Create");
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(dJ);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["PerfilId"] = new SelectList(_context.Tperfis, "ID", "ID", dJ.PerfilId);
                return View(dJ);
            }
        }

        // GET: DJs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("ADMIN") == "" || HttpContext.Session.GetString("ADMIN") == null && HttpContext.Session.GetString("UTILIZADOR") == "" || HttpContext.Session.GetString("UTILIZADOR") == null)
            {
                HttpContext.Session.SetString("CONTROLADOR", "DJs/Edit");
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var dJ = await _context.Tdjs.FindAsync(id);
                if (dJ == null)
                {
                    return NotFound();
                }
                ViewData["PerfilId"] = new SelectList(_context.Tperfis, "ID", "ID", dJ.PerfilId);
                return View(dJ);
            }
        }

        // POST: DJs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Email,UserName,Password,confirmPassword,PerfilId")] DJ dJ)
        {
            if (HttpContext.Session.GetString("ADMIN") == "" || HttpContext.Session.GetString("ADMIN") == null && HttpContext.Session.GetString("UTILIZADOR") == "" || HttpContext.Session.GetString("UTILIZADOR") == null)
            {
                HttpContext.Session.SetString("CONTROLADOR", "DJs/Edit");
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (id != dJ.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(dJ);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!DJExists(dJ.ID))
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
                ViewData["PerfilId"] = new SelectList(_context.Tperfis, "ID", "ID", dJ.PerfilId);
                return View(dJ);
            }
        }

        // GET: DJs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("ADMIN") == "" || HttpContext.Session.GetString("ADMIN") == null && HttpContext.Session.GetString("UTILIZADOR") == "" || HttpContext.Session.GetString("UTILIZADOR") == null)
            {
                HttpContext.Session.SetString("CONTROLADOR", "DJs/Delete");
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var dJ = await _context.Tdjs
                    .Include(d => d.Perfil)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (dJ == null)
                {
                    return NotFound();
                }

                return View(dJ);
            }
        }

        // POST: DJs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("ADMIN") == "" || HttpContext.Session.GetString("ADMIN") == null && HttpContext.Session.GetString("UTILIZADOR") == "" || HttpContext.Session.GetString("UTILIZADOR") == null)
            {
                HttpContext.Session.SetString("CONTROLADOR", "DJs/Delete");
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var dJ = await _context.Tdjs.FindAsync(id);
                if (dJ != null)
                {
                    _context.Tdjs.Remove(dJ);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool DJExists(int id)
        {
            return _context.Tdjs.Any(e => e.ID == id);
        }
    }
}
