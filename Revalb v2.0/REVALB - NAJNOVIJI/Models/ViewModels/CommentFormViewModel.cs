using System.ComponentModel.DataAnnotations;

namespace REVALB.Models.ViewModels
{
    public class CommentFormViewModel
    {
        [Required]
        [MaxLength(500)]
        public string Text { get; set; }

        public int ReviewId { get; set; }
        public int AlbumId { get; set; } // koristi se za redirect nazad
    }
}
