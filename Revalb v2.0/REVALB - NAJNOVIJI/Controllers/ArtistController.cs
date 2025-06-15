using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REVALB.Data;
using REVALB.Models;
using Microsoft.AspNetCore.Identity;

namespace REVALB.Controllers
{
    [Authorize(Roles = "Artist, Admin")]
    public class ArtistController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ArtistController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Analytics()
        {
            var user = await _userManager.GetUserAsync(User);

            var albums = await _context.Albums
                .Where(a => a.ArtistId == user.Id)
                .Include(a => a.AnalyticsData)
                .ToListAsync();

            return View(albums);
        }

        public async Task<IActionResult> ArtistPanel()
        {
            return View();
        }
    }
}
