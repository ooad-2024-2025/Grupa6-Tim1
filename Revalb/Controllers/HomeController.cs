using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Revalb.Models;
using Revalb.Services;

namespace Revalb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LastFmService _lastFmService;
        private readonly MusicNewsService _musicNewsService;

        public HomeController(
            ILogger<HomeController> logger,
            LastFmService lastFmService,
            MusicNewsService musicNewsService)
        {
            _logger = logger;
            _lastFmService = lastFmService;
            _musicNewsService = musicNewsService;
        }

        public async Task<IActionResult> Index()
        {
            var topTracks = await _lastFmService.GetTopTracksAsync();
            var topArtists = await _lastFmService.GetTopArtistsAsync();
            var news = await _musicNewsService.GetMusicNewsAsync();

            var viewModel = new ChartViewModel
            {
                TopTracks = topTracks,
                TopArtists = topArtists,
                News = news
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}