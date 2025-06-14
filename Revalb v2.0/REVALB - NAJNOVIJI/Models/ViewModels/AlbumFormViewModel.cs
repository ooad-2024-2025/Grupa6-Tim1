using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace REVALB.Models
{
    public class AlbumFormViewModel
    {
        public int? Id { get; set; }

        [Required]
        [RegularExpression(@"[ |a-z|A-Z|0-9|!\$]*", ErrorMessage = "Only aplhanumerical and ! $ characters are allowed!")]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Cover Image")]
        public IFormFile? CoverImageFile { get; set; }

        [Required]
        [Display(Name = "Audio Preview URL")]
        [RegularExpression(@"^https:\/\/open\.spotify\.com\/.*$", ErrorMessage = "Only spotify links allowed: https://open.spotify.com/")]
        public string AudioPreviewURL { get; set; }

        [Required]
        [Display(Name = "Scheduled Release Date")]
        public DateTime ScheduledReleaseDate { get; set; }

        public List<int> SelectedCategoryIds { get; set; } = new();

        public List<SelectListItem> Categories { get; set; } = new();
    }
}
