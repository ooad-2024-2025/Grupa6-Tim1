using Revalb.Models;

namespace REVALB.Models.ViewModels
{
    public class FullHomeViewModel
    {
        public AlbumFilterViewModel AlbumFilter { get; set; } = new();
        public List<TrackInfo> TopTracks { get; set; } = new();
        public List<TrackInfo> TopArtists { get; set; } = new();
        public List<NewsArticle> News { get; set; } = new();
    }
}