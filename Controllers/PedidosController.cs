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
            var applicationDbContext = _context.Tpedidos.Include(p => p.DJ).Include(p => p.MusicaInPlayList).Include(p => p.Utilizador);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Tpedidos
                .Include(p => p.DJ)
                .Include(p => p.MusicaInPlayList)
                .Include(p => p.Utilizador)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            ViewData["DJId"] = new SelectList(_context.Tdjs, "ID", "UserName");
            ViewData["MusicaInPlayListId"] = new SelectList(_context.TmusicaInPlayLists, "ID", "ID");
            ViewData["UtilizadorId"] = new SelectList(_context.Tutilizadores, "ID", "UserName");
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Data,Custo_Pedido,Estado,UtilizadorId,DJId,MusicaInPlayListId")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DJId"] = new SelectList(_context.Tdjs, "ID", "UserName", pedido.DJId);
            ViewData["MusicaInPlayListId"] = new SelectList(_context.TmusicaInPlayLists, "ID", "ID", pedido.MusicaInPlayListId);
            ViewData["UtilizadorId"] = new SelectList(_context.Tutilizadores, "ID", "UserName", pedido.UtilizadorId);
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Tpedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["DJId"] = new SelectList(_context.Tdjs, "ID", "UserName", pedido.DJId);
            ViewData["MusicaInPlayListId"] = new SelectList(_context.TmusicaInPlayLists, "ID", "ID", pedido.MusicaInPlayListId);
            ViewData["UtilizadorId"] = new SelectList(_context.Tutilizadores, "ID", "UserName", pedido.UtilizadorId);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Data,Custo_Pedido,Estado,UtilizadorId,DJId,MusicaInPlayListId")] Pedido pedido)
        {
            if (id != pedido.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.ID))
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
            ViewData["DJId"] = new SelectList(_context.Tdjs, "ID", "ID", pedido.DJId);
            ViewData["MusicaInPlayListId"] = new SelectList(_context.TmusicaInPlayLists, "ID", "ID", pedido.MusicaInPlayListId);
            ViewData["UtilizadorId"] = new SelectList(_context.Tutilizadores, "ID", "ID", pedido.UtilizadorId);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Tpedidos
                .Include(p => p.DJ)
                .Include(p => p.MusicaInPlayList)
                .Include(p => p.Utilizador)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Tpedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Tpedidos.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Tpedidos.Any(e => e.ID == id);
        }
    }
}
