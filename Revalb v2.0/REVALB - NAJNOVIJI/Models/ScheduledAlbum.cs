namespace REVALB.Models
{
    public class ScheduledAlbum
    {
        public int Id { get; set; }

        public DateTime ScheduledFor { get; set; }

        public int AlbumId { get; set; }

        public Album Album { get; set; }
    }
}
