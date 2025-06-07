using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Revalb.Data;
using Revalb.Models;
using Microsoft.AspNetCore.Identity;

namespace Revalb.Controllers
{
    public class KorisniksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KorisniksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Korisniks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.OfType<Korisnik>().ToListAsync());
        }

        // GET: Korisniks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Users
                .OfType<Korisnik>()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        // GET: Korisniks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Korisniks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ime,Prezime,Nadimak,Slika,brojRecenzija,UserName,Email")] Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                korisnik.Id = Guid.NewGuid().ToString(); // Generate GUID for Identity ID
                _context.Add(korisnik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(korisnik);
        }

        // GET: Korisniks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Users
                .OfType<Korisnik>()
                .FirstOrDefaultAsync(k => k.Id == id);

            if (korisnik == null)
            {
                return NotFound();
            }
            return View(korisnik);
        }

        // POST: Korisniks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Ime,Prezime,Nadimak,Slika,brojRecenzija,UserName,Email")] Korisnik korisnik)
        {
            if (id != korisnik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korisnik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KorisnikExists(korisnik.Id))
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
            return View(korisnik);
        }

        // GET: Korisniks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Users
                .OfType<Korisnik>()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        // POST: Korisniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var korisnik = await _context.Users
                .OfType<Korisnik>()
                .FirstOrDefaultAsync(k => k.Id == id);

            if (korisnik != null)
            {
                _context.Users.Remove(korisnik);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool KorisnikExists(string id)
        {
            return _context.Users.OfType<Korisnik>().Any(e => e.Id == id);
        }
    }
}
