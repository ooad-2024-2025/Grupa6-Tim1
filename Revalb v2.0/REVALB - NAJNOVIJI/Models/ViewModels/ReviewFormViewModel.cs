using System.ComponentModel.DataAnnotations;

namespace REVALB.Models.ViewModels
{
    public class ReviewFormViewModel
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string? Text { get; set; }

        public int AlbumId { get; set; }
    }
}
