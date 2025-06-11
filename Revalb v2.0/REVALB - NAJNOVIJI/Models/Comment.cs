using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace REVALB.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Text { get; set; }   

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }

        public User User { get; set; }

        public int ReviewId { get; set; }

        public Review Review { get; set; }

    }
}
