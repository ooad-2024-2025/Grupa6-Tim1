using System.ComponentModel.DataAnnotations;

namespace REVALB.Models
{
    public class Review
    {
        public int Id { get; set; }

        [MaxLength(500)]
        public string? Text { get; set; }

        [Range(1,5)]
        public int? Rating { get; set; }   

        public int UserId { get; set; }

        public User User { get; set; }

        public int AlbumId { get; set; }

        public Album Album { get; set; }

        public ICollection<Comment> Comments { get; set; }  
    }
}
