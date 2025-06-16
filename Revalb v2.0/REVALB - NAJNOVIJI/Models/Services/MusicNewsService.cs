using System.Text.Json;
using Revalb.Models;
using REVALB.Models;

namespace REVALB.Services
{
    public class MusicNewsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "5e99bcc1e6744cef9b76cde40a94976d";

        public MusicNewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "RevalbApp/1.0"); // 🔥 OVO JE KLJUČNO!
        }

        public async Task<List<NewsArticle>> GetMusicNewsAsync()
        {
            var url = $"https://newsapi.org/v2/everything?q=music&language=en&pageSize=6&sortBy=publishedAt&apiKey={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine("API RESPONSE: " + content);

            if (!response.IsSuccessStatusCode) return new();

            using var doc = JsonDocument.Parse(content);
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