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
    // CRUD de Utilizadores para Admins
    public class UtilizadoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UtilizadoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Utilizadores
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                var applicationDbContext = _context.Tutilizadores.Include(u => u.Perfil);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: Utilizadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id == null)
                {
                    TempData["Message"] = "Error: Utilizador nao Encontrado!";
                    return NotFound();
                }

                var utilizador = await _context.Tutilizadores
                    .Include(u => u.Perfil)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (utilizador == null)
                {
                    TempData["Message"] = "Error: Utilizador nao Encontrado!";
                    return NotFound();
                }
                TempData["Message"] = "Success: Utilizador encontrado com Sucesso!";
                return View(utilizador);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: Utilizadores/Create
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

        // POST: Utilizadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Email,UserName,Password,confirmPassword,Is_Admin,PerfilId")] Utilizador utilizador)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (ModelState.IsValid)
                {
                    _context.Add(utilizador);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Success: Utilizador criado com Sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                ViewData["PerfilId"] = new SelectList(_context.Tperfis, "ID", "Tipo_Perfil", utilizador.PerfilId);
                return View(utilizador);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: Utilizadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var utilizador = await _context.Tutilizadores.FindAsync(id);
                if (utilizador == null)
                {
                    return NotFound();
                }
                ViewData["PerfilId"] = new SelectList(_context.Tperfis, "ID", "Tipo_Perfil", utilizador.PerfilId);
                return View(utilizador);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: Utilizadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Email,UserName,Password,confirmPassword,Is_Admin,PerfilId")] Utilizador utilizador)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id != utilizador.ID)
            {
                    TempData["Message"] = "Error: Utilizador nao Encontrado!";
                    return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utilizador);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Success: Utilizador atualizado com Sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilizadorExists(utilizador.ID))
                    {
                        TempData["Message"] = "Error: Utilizador nao Encontrado!";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                    return RedirectToAction(nameof(Index));
            }
            ViewData["PerfilId"] = new SelectList(_context.Tperfis, "ID", "Tipo_Perfil", utilizador.PerfilId);
            return View(utilizador);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: Utilizadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var utilizador = await _context.Tutilizadores
                    .Include(u => u.Perfil)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (utilizador == null)
                {
                    return NotFound();
                }

                return View(utilizador);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: Utilizadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                var utilizador = await _context.Tutilizadores.FindAsync(id);
                if (utilizador != null)
                {
                    _context.Tutilizadores.Remove(utilizador);
                }
                try
                {
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Success: Utilizador removido com sucesso!";
                }
                catch (DbUpdateException)
                {
                    TempData["Message"] = "Error: Este Utilizador nao pode ser removido! Por Favor, Contacte Admnistrador!";
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

        private bool UtilizadorExists(int id)
        {
            return _context.Tutilizadores.Any(e => e.ID == id);
        }
    }
}
