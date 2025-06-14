using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace REVALB.Models
{
    public class Album
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Cover Image URL")]
        public string CoverImageURL { get; set; }

        [NotMapped]
        public DateTime? ReleaseDate => ScheduledAlbum?.ScheduledFor;

        [Required]
        [Display(Name = "Audio Preview URL")]
        [RegularExpression(@"^https:\/\/open\.spotify\.com\/.*$", ErrorMessage = "Only spotify links allowed: https://open.spotify.com/")]
        public string AudioPreviewURL { get; set; }

        public int ArtistId { get; set; }

        public User? Artist { get; set; }

        public ICollection<Review>? Reviews { get; set; }

        public ICollection<Category> Categories { get; set; } = new List<Category>();

        public ScheduledAlbum? ScheduledAlbum { get; set; }

        public AnalyticsData? AnalyticsData { get; set; }
    }
}
