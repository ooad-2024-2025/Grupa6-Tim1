using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Revalb.Models;

public class Album
{
    [Key]
    public int IdAlbum { get; set; }
    [ForeignKey("Korisnik")]
    public int IdArtist { get; set; }

    [Required]
    [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Album name should be in between 2 and 50 characters!")]
    [RegularExpression(@"[0-9| |a-z|A-Z|]*", ErrorMessage = "Invalid characters!")] //ovo treba dodatno editovati!!
    [DisplayName("Album name:")]
    public string Naziv { get; set; }
    public DateTime DatumObjave { get; set; }
    [EnumDataType(typeof(Zanrovi))] public Zanrovi Zanr { get; set; }
    public string CoverSlika { get; set; }
    public string ArtistIme { get; set; }
    [Required]
    [StringLength(maximumLength: 200, MinimumLength = 0, ErrorMessage = "Description is exceeding maximum number of characters (200)!")]
    [DisplayName("Description:")]
    public string Opis { get; set; } 
    public float ProsjecnaOcjena { get; set; }
    
}