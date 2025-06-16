using Newtonsoft.Json.Linq;
using Revalb.Models;

namespace Revalb.Services
{
    public class LastFmService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "5b76423b4630a9db6b41189f3387d5a3"; 


        public LastFmService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TrackInfo>> GetTopTracksAsync()
        {
            var url = $"http://ws.audioscrobbler.com/2.0/?method=chart.gettoptracks&api_key={_apiKey}&format=json&limit=10";
            var response = await _httpClient.GetStringAsync(url);
            var json = JObject.Parse(response);

            var tracks = json["tracks"]?["track"]?.Select((t, index) => new TrackInfo
            {
                Position = index + 1,
                Title = t["name"]?.ToString() ?? "",
                Artist = t["artist"]?["name"]?.ToString() ?? "",
                ImageUrl = t["image"]?.LastOrDefault()?["#text"]?.ToString() ?? ""
            }).ToList();

            return tracks ?? new List<TrackInfo>();
        }

        public async Task<List<TrackInfo>> GetTopArtistsAsync()
        {
            var url = $"http://ws.audioscrobbler.com/2.0/?method=chart.gettopartists&api_key={_apiKey}&format=json&limit=10";
            var response = await _httpClient.GetStringAsync(url);
            var json = JObject.Parse(response);

            var artists = json["artists"]?["artist"]?.Select((a, index) => new TrackInfo
            {
                Position = index + 1,
                Title = a["name"]?.ToString() ?? "",
                Artist = a["name"]?.ToString() ?? "",
                ImageUrl = a["image"]?.LastOrDefault()?["#text"]?.ToString() ?? ""
            }).ToList();

            return artists ?? new List<TrackInfo>();
        }
    }
}