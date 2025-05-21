using System;
using System.Collections.Generic;

namespace Revalb.Controllers;

public partial class Recenzije
{
    public int IdRecenzija { get; set; }

    public int IdRecenzent { get; set; }

    public int IdAlbum { get; set; }

    public string Komentar { get; set; } = null!;

    public int Zvjezdice { get; set; }

    public DateTime DatumObjave { get; set; }
}
