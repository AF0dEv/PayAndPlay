using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PayAndPlay.Data;
using PayAndPlay.Models;

namespace PayAndPlay.Controllers
{
    // CRUD Criado para o Admin gerenciar os DJs, onde ele pode adicionar, editar e remover DJs, MVC Controller
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
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                var applicationDbContext = _context.Tdjs.Include(d => d.Perfil);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: DJs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id == null)
                {
                    TempData["Message"] = "Error: DJ nao encontrado!";
                    return NotFound();
                }

                var dJ = await _context.Tdjs
                    .Include(d => d.Perfil)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (dJ == null)
                {
                    TempData["Message"] = "Error: DJ nao encontrado!";
                    return NotFound();
                }
                TempData["Message"] = "Success: DJ encontrado com Sucesso!";
                return View(dJ);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: DJs/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                ViewData["PerfilId"] = new SelectList(_context.Tperfis, "ID", "Tipo_Perfil");
                return View();
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: DJs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Email,UserName,Password,confirmPassword,PerfilId")] DJ dJ)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (ModelState.IsValid)
                {
                    _context.Add(dJ);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Success: DJ adicionado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                ViewData["PerfilId"] = new SelectList(_context.Tperfis, "ID", "ID", dJ.PerfilId);
                return View(dJ);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: DJs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id == null)
                {
                    TempData["Message"] = "Error: DJ nao encontrado!";
                    return NotFound();
                }

                var dJ = await _context.Tdjs.FindAsync(id);
                if (dJ == null)
                {
                    TempData["Message"] = "Error: DJ nao encontrado!";
                    return NotFound();
                }
                ViewData["PerfilId"] = new SelectList(_context.Tperfis, "ID", "Tipo_Perfil", dJ.PerfilId);
                return View(dJ);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: DJs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Email,UserName,Password,confirmPassword,PerfilId")] DJ dJ)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id != dJ.ID)
                {
                    TempData["Message"] = "Error: DJ nao encontrado!";
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(dJ);
                        await _context.SaveChangesAsync();
                        TempData["Message"] = "Success: DJ atualizado com sucesso!";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!DJExists(dJ.ID))
                        {
                            TempData["Message"] = "Error: DJ nao encontrado!";
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
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: DJs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id == null)
                {
                    TempData["Message"] = "Error: DJ nao encontrado!";
                    return NotFound();
                }

                var dJ = await _context.Tdjs
                    .Include(d => d.Perfil)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (dJ == null)
                {
                    TempData["Message"] = "Error: DJ nao encontrado!";
                    return NotFound();
                }

                return View(dJ);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: DJs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                var dJ = await _context.Tdjs.FindAsync(id);
                if (dJ != null)
                {
                    _context.Tdjs.Remove(dJ);
                }
                try
                {
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Success: DJ removido com sucesso!";
                }
                catch (DbUpdateException)
                {
                    TempData["Message"] = "Error: Este DJ nao pode ser removido! Por Favor, Contacte Administrador!";
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        private bool DJExists(int id)
        {
            return _context.Tdjs.Any(e => e.ID == id);
        }
    }
}
