using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Revalb.Models
{
    public class Album
    {
        [Key]
        public int IdAlbum { get; set; }

        [ForeignKey("Korisnik")]
        public string IdArtist { get; set; }

        [Required(ErrorMessage = "Naziv albuma je obavezan.")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Datum objave je obavezan.")]
        [DataType(DataType.Date)]
        public DateTime DatumObjave { get; set; }

        [Required(ErrorMessage = "Cover slika je obavezna.")]
        public string CoverSlika { get; set; }

        public string ArtistIme { get; set; }

        public string Opis { get; set; }

        public float ProsjecnaOcjena { get; set; } = 0;
    }

}