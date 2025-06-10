using System.Net.Http;
using System.Text.Json;
using Revalb.Models;

namespace Revalb.Services
{
    public class MusicNewsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "5e99bcc1e6744cef9b76cde40a94976d";

        public MusicNewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<NewsArticle>> GetMusicNewsAsync()
        {
            var url = $"https://newsapi.org/v2/everything?q=music&language=en&pageSize=6&sortBy=popularity&apiKey=5e99bcc1e6744cef9b76cde40a94976d";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) return new();

            var stream = await response.Content.ReadAsStreamAsync();

            using var doc = await JsonDocument.ParseAsync(stream);
            var articles = new List<NewsArticle>();

            foreach (var item in doc.RootElement.GetProperty("articles").EnumerateArray())
            {
                articles.Add(new NewsArticle
                {
                    Title = item.GetProperty("title").GetString() ?? "",
                    Description = item.GetProperty("description").GetString() ?? "",
                    ImageUrl = item.GetProperty("urlToImage").GetString() ?? "",
                    Source = item.GetProperty("source").GetProperty("name").GetString() ?? "",
                    PublishedAt = DateTime.TryParse(item.GetProperty("publishedAt").GetString(), out var dt) ? dt : DateTime.MinValue,
                    Url = item.GetProperty("url").GetString() ?? ""
                });
            }

            return articles;
        }
    }
}