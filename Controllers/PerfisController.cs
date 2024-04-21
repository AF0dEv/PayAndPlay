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
    // CRUD dos Perfis, para Admins
    public class PerfisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerfisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Perfis
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                return View(await _context.Tperfis.ToListAsync());
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: Perfis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id == null)
                {
                    TempData["Message"] = "Error: Perfil não encontrado!";
                    return NotFound();
                }

                var perfil = await _context.Tperfis
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (perfil == null)
                {
                    TempData["Message"] = "Error: Perfil não encontrado!";
                    return NotFound();
                }
                TempData["Message"] = "Success: Perfil encontrado!";
                return View(perfil);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: Perfis/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                return View();
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: Perfis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Tipo_Perfil")] Perfil perfil)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (ModelState.IsValid)
                {
                    _context.Add(perfil);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Success: Perfil criado com Sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                return View(perfil);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: Perfis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id == null)
                {
                    TempData["Message"] = "Error: Perfil não encontrado!";
                    return NotFound();
                }

                var perfil = await _context.Tperfis.FindAsync(id);
                if (perfil == null)
                {
                    TempData["Message"] = "Error: Perfil não encontrado!";
                    return NotFound();
                }
                return View(perfil);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: Perfis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Tipo_Perfil")] Perfil perfil)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id != perfil.ID)
                {
                    TempData["Message"] = "Error: Perfil não encontrado!";
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(perfil);
                        await _context.SaveChangesAsync();
                        TempData["Message"] = "Success: Perfil atualizado com Sucesso!";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PerfilExists(perfil.ID))
                        {
                            TempData["Message"] = "Error: Perfil não encontrado!";
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(perfil);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: Perfis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id == null)
                {
                    TempData["Message"] = "Error: Perfil não encontrado!";
                    return NotFound();
                }

                var perfil = await _context.Tperfis
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (perfil == null)
                {
                    TempData["Message"] = "Error: Perfil não encontrado!";
                    return NotFound();
                }

                return View(perfil);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: Perfis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                var perfil = await _context.Tperfis.FindAsync(id);
                if (perfil != null)
                {
                    _context.Tperfis.Remove(perfil);
                }
                try
                {
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Success: Perfil removido com sucesso!";
                }
                catch (DbUpdateException)
                {
                    TempData["Message"] = "Error: Este Perfil nao pode ser removido! Por Favor, Contacte Administrador!";
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

        private bool PerfilExists(int id)
        {
            return _context.Tperfis.Any(e => e.ID == id);
        }
    }
}
