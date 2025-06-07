using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Revalb.Data;
using Revalb.Models;

namespace Revalb.Controllers
{
    [Authorize]
    public class RecenzijasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecenzijasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recenzijas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recenzije.ToListAsync());
        }

        // GET: Recenzijas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzija = await _context.Recenzije
                .FirstOrDefaultAsync(m => m.idRecenzija == id);
            if (recenzija == null)
            {
                return NotFound();
            }

            return View(recenzija);
        }

        // GET: Recenzijas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recenzijas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idRecenzija,idRecenzent,idAlbum,Komentar,Zvjezdice,DatumObjave")] Recenzija recenzija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recenzija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recenzija);
        }

        // GET: Recenzijas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzija = await _context.Recenzije.FindAsync(id);
            if (recenzija == null)
            {
                return NotFound();
            }
            return View(recenzija);
        }

        // POST: Recenzijas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idRecenzija,idRecenzent,idAlbum,Komentar,Zvjezdice,DatumObjave")] Recenzija recenzija)
        {
            if (id != recenzija.idRecenzija)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recenzija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecenzijaExists(recenzija.idRecenzija))
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
            return View(recenzija);
        }

        // GET: Recenzijas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzija = await _context.Recenzije
                .FirstOrDefaultAsync(m => m.idRecenzija == id);
            if (recenzija == null)
            {
                return NotFound();
            }

            return View(recenzija);
        }

        // POST: Recenzijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recenzija = await _context.Recenzije.FindAsync(id);
            if (recenzija != null)
            {
                _context.Recenzije.Remove(recenzija);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecenzijaExists(int id)
        {
            return _context.Recenzije.Any(e => e.idRecenzija == id);
        }
    }
}
