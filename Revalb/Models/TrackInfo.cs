namespace Revalb.Models;

public class TrackInfo
{
    public int Position { get; set; }
    public string Title { get; set; } = "";
    public string Artist { get; set; } = "";
    public string ImageUrl { get; set; } = "";
}

public class ChartViewModel
{
    public List<TrackInfo> TopGlobal { get; set; } = new();
    public List<TrackInfo> TopBH { get; set; } = new();
    public List<TrackInfo> RevalbSpecials { get; set; } = new();
}
