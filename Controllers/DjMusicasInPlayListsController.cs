using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PayAndPlay.Data;
using PayAndPlay.Models;

namespace PayAndPlay.Controllers
{
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
            var applicationDbContext = _context.TmusicaInPlayLists.Include(m => m.Musica).Include(m => m.PlayList).Where(p => p.PlayList.DJId == int.Parse(HttpContext.Session.GetString("ID")));
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MusicasInPlayLists/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: MusicasInPlayLists/Create
        public IActionResult Create()
        {
            ViewData["MusicaId"] = new SelectList(_context.Tmusicas, "Id", "Nome");
            ViewData["PlayListId"] = new SelectList(_context.TplayLists.Where(p => p.DJId == int.Parse(HttpContext.Session.GetString("ID"))), "ID", "Nome");
            return View();
        }

        // POST: MusicasInPlayLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MusicaId,PlayListId")] MusicaInPlayList musicaInPlayList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(musicaInPlayList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MusicaId"] = new SelectList(_context.Tmusicas, "Id", "Nome", musicaInPlayList.MusicaId);
            ViewData["PlayListId"] = new SelectList(_context.TplayLists.Where(p => p.DJId == int.Parse(HttpContext.Session.GetString("ID"))), "ID", "Nome", musicaInPlayList.PlayListId);
            return View(musicaInPlayList);
        }

        // GET: MusicasInPlayLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["MusicaId"] = new SelectList(_context.Tmusicas, "Id", "Nome", musicaInPlayList.MusicaId);
            ViewData["PlayListId"] = new SelectList(_context.TplayLists.Where(p => p.DJId == int.Parse(HttpContext.Session.GetString("ID"))), "ID", "Nome", musicaInPlayList.PlayListId);
            return View(musicaInPlayList);
        }

        // POST: MusicasInPlayLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MusicaId,PlayListId")] MusicaInPlayList musicaInPlayList)
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
            ViewData["MusicaId"] = new SelectList(_context.Tmusicas, "Id", "Nome", musicaInPlayList.MusicaId);
            ViewData["PlayListId"] = new SelectList(_context.TplayLists.Where(p => p.DJId == int.Parse(HttpContext.Session.GetString("ID"))), "ID", "Nome", musicaInPlayList.PlayListId);
            return View(musicaInPlayList);
        }

        // GET: MusicasInPlayLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: MusicasInPlayLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musicaInPlayList = await _context.TmusicaInPlayLists.FindAsync(id);
            if (musicaInPlayList != null)
            {
                _context.TmusicaInPlayLists.Remove(musicaInPlayList);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicaInPlayListExists(int id)
        {
            return _context.TmusicaInPlayLists.Any(e => e.ID == id);
        }
    }
}

