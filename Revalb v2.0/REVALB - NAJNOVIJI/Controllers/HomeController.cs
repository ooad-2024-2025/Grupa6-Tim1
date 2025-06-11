using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using REVALB.Data;
using REVALB.Models;
using REVALB.Models.ViewModels;

namespace REVALB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(string? searchTerm, int? selectedCategoryId)
        {
            var albumsQuery = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.ScheduledAlbum)
                .Include(a => a.Categories)
                .Include(a => a.Reviews)
                .Where(a => a.ScheduledAlbum == null || a.ScheduledAlbum.ScheduledFor.Date <= DateTime.Today);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                albumsQuery = albumsQuery.Where(a =>
                    a.Title.Contains(searchTerm) ||
                    a.Description.Contains(searchTerm));
            }

            if (selectedCategoryId.HasValue)
            {
                albumsQuery = albumsQuery.Where(a =>
                    a.Categories.Any(c => c.Id == selectedCategoryId.Value));
            }

            var albums = await albumsQuery
                .OrderByDescending(a => a.ScheduledAlbum.ScheduledFor)
                .ToListAsync();

            var viewModel = new AlbumFilterViewModel
            {
                SearchTerm = searchTerm,
                SelectedCategoryId = selectedCategoryId,
                Categories = await _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToListAsync(),
                Albums = albums
            };

            return View(viewModel);
        }

        /*
        public async Task<IActionResult> Index()
        {
            var albums = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.ScheduledAlbum)
                .Include(a => a.Categories)
                .Where(a =>
                    a.ScheduledAlbum == null ||
                    a.ScheduledAlbum.ScheduledFor.Date <= DateTime.Today)
                .OrderByDescending(a => a.ScheduledAlbum.ScheduledFor)
                .ToListAsync();

            return View(albums); 
        }
        */

        /*
        public async Task<IActionResult> Index(string? searchTerm, int? selectedCategoryId)
        {
            var albumsQuery = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.ScheduledAlbum)
                .Include(a => a.Categories)
                .Where(a =>
                    a.ScheduledAlbum == null ||
                    a.ScheduledAlbum.ScheduledFor.Date <= DateTime.Today);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                albumsQuery = albumsQuery.Where(a =>
                    a.Title.Contains(searchTerm) ||
                    a.Description.Contains(searchTerm));
            }

            if (selectedCategoryId.HasValue)
            {
                albumsQuery = albumsQuery.Where(a =>
                    a.Categories.Any(c => c.Id == selectedCategoryId.Value));
            }

            var albums = await albumsQuery
                .OrderByDescending(a => a.ScheduledAlbum.ScheduledFor)
                .ToListAsync();

            var viewModel = new AlbumFilterViewModel
            {
                SearchTerm = searchTerm,
                SelectedCategoryId = selectedCategoryId,
                Categories = await _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToListAsync(),
                Albums = albums
            };

            return View(viewModel);
        }

        */
        /*
        public async Task<IActionResult> Index()
        {
            var albums = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.ScheduledAlbum)
                .Where(a =>
                    a.ScheduledAlbum == null ||
                    a.ScheduledAlbum.ScheduledFor.Date <= DateTime.Today)
                .OrderByDescending(a => a.ScheduledAlbum.ScheduledFor)
                .ToListAsync();

            return View(albums);
        }*/

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [AllowAnonymous]
        public async Task<IActionResult> Ranking()
        {
            var topAlbums = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.ScheduledAlbum)
                .Include(a => a.Categories)
                .Include(a => a.AnalyticsData)
                .Where(a => a.ScheduledAlbum == null || a.ScheduledAlbum.ScheduledFor.Date <= DateTime.Today)
                .OrderByDescending(a => a.AnalyticsData.AverageRating)
                .ThenByDescending(a => a.AnalyticsData.ReviewCount)
                .Take(10) // top 10 albuma
                .ToListAsync();

            return View(topAlbums);
        }

    }
}
