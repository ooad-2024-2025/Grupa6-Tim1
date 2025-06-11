using Microsoft.AspNetCore.Identity;

namespace REVALB.Models
{
    public class User: IdentityUser<int>
    {
        public string? ProfilePictureURL { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Comment> Comments { get; set; }  

        public ICollection<Album> FavoriteAlbums { get; set; } //many-to-many
        
        public DateTime CreatedAt { get; set; }
    }
}
