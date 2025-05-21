using System;
using System.Collections.Generic;

namespace Revalb.Controllers;

public partial class Korisnici
{
    public int IdKorisnik { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Nadimak { get; set; } = null!;

    public string Slika { get; set; } = null!;

    public int BrojRecenzija { get; set; }
}
