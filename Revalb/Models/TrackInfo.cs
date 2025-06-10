namespace Revalb.Models
{
    public class TrackInfo
    {
        public int Position { get; set; }
        public string Title { get; set; } = "";
        public string Artist { get; set; } = "";
        public string ImageUrl { get; set; } = "";
    }

    public class ChartViewModel
    {
        public List<TrackInfo> TopTracks { get; set; } = new();
        public List<TrackInfo> TopArtists { get; set; } = new();
        
        public List<NewsArticle> News { get; set; } = new();
    }
}