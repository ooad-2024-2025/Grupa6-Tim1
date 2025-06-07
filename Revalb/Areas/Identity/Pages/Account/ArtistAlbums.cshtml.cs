using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Revalb.Data;
using Revalb.Models;

namespace Revalb.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Artist")]
    public class ArtistAlbumsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public ArtistAlbumsModel(ApplicationDbContext context, UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Album> Albums { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            Albums = _context.Albumi.Where(a => a.IdArtist == user.Id).ToList();
        }
    }
}