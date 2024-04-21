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
    // CRUD de Pedidos para Admins
    // Estados possíveis: Pendente, Pago, Recusado, Concluido, Levantamento
    public class PedidosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                var applicationDbContext = _context.Tpedidos.Include(p => p.DJ).Include(p => p.MusicaInPlayList).Include(p => p.Utilizador);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id == null)
                {
                    TempData["Message"] = "Error: Pedido não encontrado!";
                    return NotFound();
                }

                var pedido = await _context.Tpedidos
                    .Include(p => p.DJ)
                    .Include(p => p.MusicaInPlayList)
                    .Include(p => p.Utilizador)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (pedido == null)
                {
                    TempData["Message"] = "Error: Pedido não encontrado!";
                    return NotFound();
                }
                TempData["Message"] = "Success: Pedido encontrado com Sucesso!";
                return View(pedido);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                ViewData["DJId"] = new SelectList(_context.Tdjs, "ID", "UserName");
                ViewData["MusicaInPlayListId"] = new SelectList(_context.TmusicaInPlayLists, "ID", "ID");
                ViewData["UtilizadorId"] = new SelectList(_context.Tutilizadores, "ID", "UserName");
                return View();
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Data,Custo_Pedido,Estado,UtilizadorId,DJId,MusicaInPlayListId")] Pedido pedido)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (ModelState.IsValid)
                {
                    _context.Add(pedido);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Success: Pedido criado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                ViewData["DJId"] = new SelectList(_context.Tdjs, "ID", "UserName", pedido.DJId);
                ViewData["MusicaInPlayListId"] = new SelectList(_context.TmusicaInPlayLists, "ID", "ID", pedido.MusicaInPlayListId);
                ViewData["UtilizadorId"] = new SelectList(_context.Tutilizadores, "ID", "UserName", pedido.UtilizadorId);
                return View(pedido);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }
        
        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id == null)
                {
                    TempData["Message"] = "Error: Pedido não encontrado!";
                    return NotFound();
                }

                var pedido = await _context.Tpedidos.FindAsync(id);
                if (pedido == null)
                {
                    TempData["Message"] = "Error: Pedido não encontrado!";
                    return NotFound();
                }
                ViewData["DJId"] = new SelectList(_context.Tdjs, "ID", "UserName", pedido.DJId);
                ViewData["MusicaInPlayListId"] = new SelectList(_context.TmusicaInPlayLists, "ID", "ID", pedido.MusicaInPlayListId);
                ViewData["UtilizadorId"] = new SelectList(_context.Tutilizadores, "ID", "UserName", pedido.UtilizadorId);
                return View(pedido);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Data,Custo_Pedido,Estado,UtilizadorId,DJId,MusicaInPlayListId")] Pedido pedido)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id != pedido.ID)
                {
                    TempData["Message"] = "Error: Pedido não encontrado!";
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(pedido);
                        await _context.SaveChangesAsync();
                        TempData["Message"] = "Success: Pedido atualizado com sucesso!";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PedidoExists(pedido.ID))
                        {
                            TempData["Message"] = "Error: Pedido não encontrado!";
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["DJId"] = new SelectList(_context.Tdjs, "ID", "ID", pedido.DJId);
                ViewData["MusicaInPlayListId"] = new SelectList(_context.TmusicaInPlayLists, "ID", "ID", pedido.MusicaInPlayListId);
                ViewData["UtilizadorId"] = new SelectList(_context.Tutilizadores, "ID", "ID", pedido.UtilizadorId);
                return View(pedido);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                if (id == null)
                {
                    TempData["Message"] = "Error: Pedido não encontrado!";
                    return NotFound();
                }

                var pedido = await _context.Tpedidos
                    .Include(p => p.DJ)
                    .Include(p => p.MusicaInPlayList)
                    .Include(p => p.Utilizador)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (pedido == null)
                {
                    TempData["Message"] = "Error: Pedido não encontrado!";
                    return NotFound();
                }

                return View(pedido);
            }
            else
            {
                TempData["Message"] = "Error: Nao tem permissoes para aceder a esta pagina!";
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("UTILIZADOR") != "" && HttpContext.Session.GetString("UTILIZADOR") != null && HttpContext.Session.GetString("ADMIN") == "true" && HttpContext.Session.GetString("PERFIL") == "3")
            {
                var pedido = await _context.Tpedidos.FindAsync(id);
                if (pedido != null)
                {
                    _context.Tpedidos.Remove(pedido);
                }
                try
                {
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Success: Pedido removido com sucesso!";
                }
                catch (DbUpdateException)
                {
                    TempData["Message"] = "Error: Este Pedido nao pode ser removido! Por Favor, Contacte Administrador!";
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

        private bool PedidoExists(int id)
        {
            return _context.Tpedidos.Any(e => e.ID == id);
        }
    }
}
