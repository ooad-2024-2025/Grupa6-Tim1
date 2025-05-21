using System;
using System.Collections.Generic;

namespace Revalb.Controllers;

public partial class Pjesme
{
    public int IdPjesma { get; set; }

    public string Naziv { get; set; } = null!;

    public int Trajanje { get; set; }

    public int RedniBroj { get; set; }

    public string Fajl { get; set; } = null!;

    public int IdAlbum { get; set; }
}
