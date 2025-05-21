using System;
using System.Collections.Generic;

namespace Revalb.Controllers;

public partial class Albumi
{
    public int IdAlbum { get; set; }

    public int IdArtist { get; set; }

    public string Naziv { get; set; } = null!;

    public DateTime DatumObjave { get; set; }

    public string Zanr { get; set; } = null!;

    public string CoverSlika { get; set; } = null!;

    public string ArtistIme { get; set; } = null!;

    public string Opis { get; set; } = null!;

    public float ProsjecnaOcjena { get; set; }
}
