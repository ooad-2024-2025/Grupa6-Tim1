using System.ComponentModel.DataAnnotations;

namespace Revalb.Models;

public class Korisnik
{
    [Key]
    public int idKorisnik { get; set; }
    
    public string Ime{ get; set; }
    
    public string Prezime{ get; set; }
    
    public string Email{ get; set; }
    
    public string Nadimak{ get; set; }
    
    public string Slika{ get; set; }
    
    public int brojRecenzija{ get; set; }
}