using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Revalb.Models;

public class Korisnik
{
    [Key]
    public int idKorisnik { get; set; }
    
    [Required]
    [StringLength(maximumLength:15, MinimumLength = 3, ErrorMessage ="Name should be in between 3 and 15 characters!")]
    [RegularExpression(@"[a-z|A-Z|]*", ErrorMessage = "Only alphabet letters are allowed!")]
    [DisplayName("Name:")]
    public string Ime{ get; set; }
    [Required]
    [StringLength(maximumLength: 15, MinimumLength = 3, ErrorMessage = "Surname should be in between 3 and 15 characters!")]
    [RegularExpression(@"[a-z|A-Z|]*", ErrorMessage = "Only alphabet letters are allowed!")]
    [DisplayName("Surname:")]
    public string Prezime{ get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Insert a valid e-mail address!")]
    [DisplayName("e-mail:")]
    public string Email{ get; set; }
    [Required]
    [StringLength(maximumLength: 10, MinimumLength = 3, ErrorMessage = "Username should be in between 3 and 10 characters!")]
    [RegularExpression(@"[0-9|a-z|]*", ErrorMessage = "Please use characters from 0-9 and a-z!")]
    [DisplayName("Username:")]
    public string Nadimak{ get; set; }
    
    public string Slika{ get; set; }
    
    public int brojRecenzija{ get; set; }
}