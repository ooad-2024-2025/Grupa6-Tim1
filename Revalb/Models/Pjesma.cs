using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Revalb.Models;

public class Pjesma
{
    [Key]
    public int idPjesma { get; set; }
    [Required]
    [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Song name should be in between 2 and 50 characters!")]
    [RegularExpression(@"[0-9| |a-z|A-Z|]*", ErrorMessage = "Invalid characters!")] //ovo treba dodatno editovati!!
    [DisplayName("Song name:")]
    public string Naziv { get; set; }
    public int Trajanje { get; set; }
    public int redniBroj { get; set; }
    public string fajl { get; set; }
    
    [ForeignKey("Album")]
    public int idAlbum { get; set; }
}