using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Revalb.Data;
using Revalb.Models;

namespace Revalb.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Artist")]
    public class ArtistProfileModel : PageModel
    {
        private readonly UserManager<Korisnik> _userManager;
        private readonly ApplicationDbContext _context;

        public ArtistProfileModel(UserManager<Korisnik> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public string ArtistName { get; set; }
        public int MonthlyListeners { get; set; } = 0;  // Default je 0 (kasnije možeš dodati logiku da praviš random ili pravi broj)
        public List<Album> Albums { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            ArtistName = user.Ime + " " + user.Prezime;

            // Dohvati sve albume gdje je IdArtist == user.Id
            Albums = _context.Albumi
                .Where(a => a.IdArtist == user.Id)
                .ToList();

            // Monthly listeners logika (dummy vrijednost za sada, možeš kasnije napraviti stvarne podatke)
            MonthlyListeners = new Random().Next(10000, 1000000);
        }
    }
}