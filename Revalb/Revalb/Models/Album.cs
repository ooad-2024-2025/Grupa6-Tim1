using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Revalb.Models;

public class Album
{
    [Key]
    public int IdAlbum { get; set; }
    [ForeignKey("Korisnik")]
    public int IdArtist { get; set; }
    
    public string Naziv { get; set; } 
    public DateTime DatumObjave { get; set; }
    public List<Zanrovi> Zanr { get; set; }
    public string CoverSlika { get; set; }
    public string ArtistIme { get; set; }
    public string Opis { get; set; } 
    public float ProsjecnaOcjena { get; set; }
    
}