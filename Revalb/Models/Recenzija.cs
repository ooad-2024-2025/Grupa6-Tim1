using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Revalb.Models;

public class Recenzija
{
    [Key]
    public int idRecenzija { get; set; }
    
    [ForeignKey("Korisnik")]
    public int idRecenzent { get; set; }
    
    [ForeignKey("Album")]
    public int idAlbum { get; set; }

    [Required]
    [StringLength(maximumLength: 300, MinimumLength = 1, ErrorMessage = "Comment should be in between 1 and 300 characters!")]
    [DisplayName("Leave a comment:")]
    public string Komentar { get; set; }
    public int Zvjezdice { get; set; }
    public DateTime DatumObjave { get; set; }
}