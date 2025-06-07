using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Revalb.Models
{
    public class Korisnik : IdentityUser
    {
        [Required]
        public string Ime { get; set; }

        [Required]
        public string Prezime { get; set; }

        // Nadimak, Slika -> Nullable
        public string? Nadimak { get; set; }

        public string? Slika { get; set; }

        // brojRecenzija -> OK da ostane int, ali možeš staviti default vrijednost
        public int brojRecenzija { get; set; } = 0;
    }
}