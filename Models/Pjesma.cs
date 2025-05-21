using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Revalb.Models;

public class Pjesma
{
    [Key]
    public int idPjesma { get; set; }
    
    public string naziv { get; set; }
    public int Trajanje { get; set; }
    public int redniBroj { get; set; }
    public string fajl { get; set; }
    
    [ForeignKey("Album")]
    public int idAlbum { get; set; }
}