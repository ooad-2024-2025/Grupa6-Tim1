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
    [Authorize(Roles = "Administrator")]
    public class PjesmasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PjesmasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pjesmas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pjesme.ToListAsync());
        }

        // GET: Pjesmas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pjesma = await _context.Pjesme
                .FirstOrDefaultAsync(m => m.idPjesma == id);
            if (pjesma == null)
            {
                return NotFound();
            }

            return View(pjesma);
        }

        // GET: Pjesmas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pjesmas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idPjesma,naziv,Trajanje,redniBroj,fajl,idAlbum")] Pjesma pjesma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pjesma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pjesma);
        }

        // GET: Pjesmas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pjesma = await _context.Pjesme.FindAsync(id);
            if (pjesma == null)
            {
                return NotFound();
            }
            return View(pjesma);
        }

        // POST: Pjesmas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idPjesma,naziv,Trajanje,redniBroj,fajl,idAlbum")] Pjesma pjesma)
        {
            if (id != pjesma.idPjesma)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pjesma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PjesmaExists(pjesma.idPjesma))
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
            return View(pjesma);
        }

        // GET: Pjesmas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pjesma = await _context.Pjesme
                .FirstOrDefaultAsync(m => m.idPjesma == id);
            if (pjesma == null)
            {
                return NotFound();
            }

            return View(pjesma);
        }

        // POST: Pjesmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pjesma = await _context.Pjesme.FindAsync(id);
            if (pjesma != null)
            {
                _context.Pjesme.Remove(pjesma);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PjesmaExists(int id)
        {
            return _context.Pjesme.Any(e => e.idPjesma == id);
        }
    }
}
