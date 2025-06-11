namespace REVALB.Models
{
    public class AnalyticsData
    {
        public int Id { get; set; }

        public int AlbumId { get; set; }

        public Album Album { get; set; }

        public int ClickCount { get; set; } = 0;

        public int ProfileViews { get; set; } = 0;

        public int ReviewCount { get; set; } = 0;

        public double AverageRating { get; set; } = 0.0;
    }
}
