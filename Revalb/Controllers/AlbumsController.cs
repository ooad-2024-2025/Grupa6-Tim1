using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Revalb.Data;
using Revalb.Models;

namespace Revalb.Controllers
{
    [Authorize(Roles = "Artist, Administrator")]
    public class AlbumsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public AlbumsController(ApplicationDbContext context, UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Albums
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (await _userManager.IsInRoleAsync(user, "Administrator"))
            {
                // Admin vidi sve albume
                return View(await _context.Albumi.ToListAsync());
            }
            else
            {
                // Artist vidi samo svoje albume
                return View(await _context.Albumi
                    .Where(a => a.IdArtist == user.Id)
                    .ToListAsync());
            }
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var album = await _context.Albumi.FirstOrDefaultAsync(m => m.IdAlbum == id);
            if (album == null || !await IsOwner(album)) return NotFound();

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Naziv,DatumObjave,CoverSlika,Opis")] Album album)
        {
            var user = await _userManager.GetUserAsync(User);
            album.IdArtist = user.Id;
            album.ArtistIme = user.Ime + " " + user.Prezime;
            album.ProsjecnaOcjena = 0;

            ModelState.Clear();
            TryValidateModel(album);

            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(album);
        }


        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var album = await _context.Albumi.FindAsync(id);
            if (album == null || !await IsOwner(album)) return NotFound();

            return View(album);
        }

        // POST: Albums/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAlbum,Naziv,DatumObjave,CoverSlika,Opis")] Album album)
        {
            if (id != album.IdAlbum) return NotFound();

            var dbAlbum = await _context.Albumi.FindAsync(id);
            if (dbAlbum == null || !await IsOwner(dbAlbum)) return NotFound();

            // Ovdje popunjavamo ono što ne šaljemo kroz formu:
            album.IdArtist = dbAlbum.IdArtist;
            album.ArtistIme = dbAlbum.ArtistIme;
            album.ProsjecnaOcjena = dbAlbum.ProsjecnaOcjena;

            // ⚡ RESETUJ VALIDACIJU:
            ModelState.Clear();
            TryValidateModel(album);

            if (ModelState.IsValid)
            {
                try
                {
                    // Update polja
                    dbAlbum.Naziv = album.Naziv;
                    dbAlbum.DatumObjave = album.DatumObjave;
                    dbAlbum.CoverSlika = album.CoverSlika;
                    dbAlbum.Opis = album.Opis;

                    _context.Update(dbAlbum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.IdAlbum))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }



        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var album = await _context.Albumi.FirstOrDefaultAsync(m => m.IdAlbum == id);
            if (album == null || !await IsOwner(album)) return NotFound();

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Albumi.FindAsync(id);
            if (album == null || !await IsOwner(album)) return NotFound();

            _context.Albumi.Remove(album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
            return _context.Albumi.Any(e => e.IdAlbum == id);
        }

        private async Task<bool> IsOwner(Album album)
        {
            var user = await _userManager.GetUserAsync(User);
            return album.IdArtist == user.Id;
        }
    }
}
