using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using REVALB.Data;
using REVALB.HelperClass;
using REVALB.Models;
using REVALB.Models.ViewModels;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace REVALB.Controllers
{
    //[Authorize(Roles = "Artist")]
    public class AlbumController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public AlbumController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Album/
        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var albums = await _context.Albums
                .Where(a => a.ArtistId == user.Id)
                .ToListAsync();

            return View(albums);
        }

        // GET: /Album/Create
        [Authorize(Roles = "Artist")]
        public IActionResult Create()
        {
            var viewModel = new AlbumFormViewModel
            {
                Categories = _context.Categories
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .ToList()
            };

            return View(viewModel);
        }

        //POST: /Album/Create
        [HttpPost]
        [Authorize(Roles = "Artist")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlbumFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _context.Categories
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .ToList();
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);

            string coverImagePath = "";

            if (model.CoverImageFile != null && model.CoverImageFile.Length > 0)
            {
                var result = UploadHandler.Upload(model.CoverImageFile, "covers");
                if (!result.Success)
                {
                    ModelState.AddModelError("", result.MessageOrPath);
                    model.Categories = _context.Categories
                        .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                        .ToList();
                    return View(model);
                }

                coverImagePath = result.MessageOrPath;
            }

            var album = new Album
            {
                Title = model.Title,
                Description = model.Description,
                CoverImageURL = coverImagePath,
                AudioPreviewURL = model.AudioPreviewURL,
                ArtistId = user.Id,
                Categories = _context.Categories
                    .Where(c => model.SelectedCategoryIds.Contains(c.Id))
                    .ToList(),
                ScheduledAlbum = new ScheduledAlbum
                {
                    ScheduledFor = model.ScheduledReleaseDate
                }
            };

            _context.Albums.Add(album);
            _context.AnalyticsData.Add(new AnalyticsData
            {
                Album = album,
                ClickCount = 0,
                ProfileViews = 0,
                ReviewCount = 0,
                AverageRating = 0
            });

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        // GET: /Album/Edit/5
        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> Edit(int id)
        {
            var album = await _context.Albums.FindAsync(id);

            if (album == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (album.ArtistId != user.Id)
                return Forbid(); // zaštita: samo vlasnik može uređivati

            return View(album);
        }

        // POST: /Album/Edit/5
        [HttpPost]
        [Authorize(Roles = "Artist")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Album album, IFormFile? CoverImageFile, DateTime? ReleaseDate)
        {
            if (id != album.Id)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (album.ArtistId != user.Id)
                return Forbid();

            var existing = await _context.Albums.FirstOrDefaultAsync(a => a.Id == id);
            if (existing == null)
                return NotFound();

            if (CoverImageFile != null && CoverImageFile.Length > 0)
            {
                var result = UploadHandler.Upload(CoverImageFile, "covers");
                if (!result.Success)
                {
                    ModelState.AddModelError("", result.MessageOrPath);
                    return View(album);
                }

                existing.CoverImageURL = result.MessageOrPath;
            }

            existing.Title = album.Title;
            existing.Description = album.Description;
            existing.AudioPreviewURL = album.AudioPreviewURL;

            /*
            if (existing.ScheduledAlbum != null)
            {
                existing.ScheduledAlbum.ScheduledFor = ReleaseDate ?? DateTime.Now;

            }
            else
            {
                existing.ScheduledAlbum = new ScheduledAlbum
                {
                    ScheduledFor = album.ReleaseDate ?? DateTime.Now
                };
            }
            */

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Album/Delete/5
        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> Delete(int id)
        {
            var album = await _context.Albums.FindAsync(id);

            if (album == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (album.ArtistId != user.Id)
                return Forbid();

            return View(album);
        }

        // POST: /Album/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Artist")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Albums.FindAsync(id);

            var user = await _userManager.GetUserAsync(User);
            if (album.ArtistId != user.Id)
                return Forbid();

            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //Details
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var album = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.ScheduledAlbum)
                .Include(a => a.Categories) 
                .Include(a => a.Reviews)
                    .ThenInclude(r => r.User)
                .Include(a => a.Reviews)
                    .ThenInclude(r => r.Comments)
                        .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(a => a.Id == id);


            if (album == null)
                return NotFound();

            var analytics = await _context.AnalyticsData.FirstOrDefaultAsync(a => a.AlbumId == album.Id);
            if (analytics != null)
            {
                analytics.ClickCount += 1;
                await _context.SaveChangesAsync();
            }

            bool userAlreadyReviewed = false;

            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.Users
                    .Include(u => u.FavoriteAlbums)
                    .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

                userAlreadyReviewed = album.Reviews != null &&
                    album.Reviews.Any(r => r.UserId == user.Id);

                bool isFavorited = user.FavoriteAlbums.Any(f => f.Id == album.Id);
                ViewBag.IsFavorited = isFavorited;

            }

            ViewBag.UserAlreadyReviewed = userAlreadyReviewed;



            /*// Sakrij album ako je zakazan i korisnik nije artist
            if (album.ScheduledAlbum?.ScheduledFor.Date > DateTime.Today)
            {
                var user = await _userManager.GetUserAsync(User);
                if (!User.IsInRole("Artist") || album.ArtistId != user.Id)
                    return Forbid();
            }*/


            return View(album);
        }

        //dodaj recenziju
        [HttpPost]
        [Authorize(Roles = "User,Artist")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(ReviewFormViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Details", new { id = model.AlbumId });

            var user = await _userManager.GetUserAsync(User);

            // Jedna recenzija po korisniku po albumu
            bool alreadyExists = await _context.Reviews
                .AnyAsync(r => r.AlbumId == model.AlbumId && r.UserId == user.Id);

            if (alreadyExists)
                return RedirectToAction("Details", new { id = model.AlbumId });

            var review = new Review
            {
                AlbumId = model.AlbumId,
                UserId = user.Id,
                Rating = model.Rating,
                Text = model.Text
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            // Ažuriraj AnalyticsData
            var analytics = await _context.AnalyticsData.FirstOrDefaultAsync(a => a.AlbumId == model.AlbumId);
            if (analytics != null)
            {
                analytics.ReviewCount += 1;

                // Ponovno izračunaj prosjek svih ocjena
                var ratings = await _context.Reviews
                    .Where(r => r.AlbumId == model.AlbumId && r.Rating.HasValue)
                    .Select(r => r.Rating.Value)
                    .ToListAsync();

                if (ratings.Count > 0)
                    analytics.AverageRating = Math.Round(ratings.Average(), 2);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id = model.AlbumId });
        }
        //dodaj komentar na recenziju
        [HttpPost]
        [Authorize(Roles = "User,Artist")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(CommentFormViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Details", new { id = model.AlbumId });

            var user = await _userManager.GetUserAsync(User);

            var comment = new Comment
            {
                ReviewId = model.ReviewId,
                UserId = user.Id,
                Text = model.Text,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = model.AlbumId });
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleFavorite(int albumId)
        {
            var user = await _userManager.Users
                .Include(u => u.FavoriteAlbums)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            var album = await _context.Albums.FindAsync(albumId);

            if (album == null || user == null)
                return NotFound();

            // Provjera postoji li već veza
            bool isFavorite = user.FavoriteAlbums.Any(a => a.Id == album.Id);

            if (isFavorite)
            {
                user.FavoriteAlbums.Remove(user.FavoriteAlbums.First(a => a.Id == album.Id));
            }
            else
            {
                // Attach album ako treba
                if (_context.Entry(album).State == EntityState.Detached)
                    _context.Attach(album);

                user.FavoriteAlbums.Add(album);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = albumId });
        }

        [Authorize]
        public async Task<IActionResult> Favorites()
        {
            var user = await _userManager.Users
                .Include(u => u.FavoriteAlbums)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            var albums = user?.FavoriteAlbums?.ToList() ?? new List<Album>();

            return View(albums);
        }

    }
}