using Microsoft.AspNetCore.Mvc;
using Revalb.Models;
using Revalb.Services;

namespace Revalb.Controllers
{
    public class ChartsController : Controller
    {
        private readonly LastFmService _lastFm;

        public ChartsController(LastFmService lastFm)
        {
            _lastFm = lastFm;
        }

        public async Task<IActionResult> Index()
        {
            var tracks = await _lastFm.GetTopTracksAsync();
            var artists = await _lastFm.GetTopArtistsAsync();

            var viewModel = new ChartViewModel
            {
                TopTracks = tracks,
                TopArtists = artists
            };

            return View(viewModel);
        }
    }
}