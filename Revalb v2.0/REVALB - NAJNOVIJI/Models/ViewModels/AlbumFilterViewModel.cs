using Microsoft.AspNetCore.Mvc.Rendering;

namespace REVALB.Models.ViewModels
{
    public class AlbumFilterViewModel
    {
        public string? SearchTerm { get; set; }

        public int? SelectedCategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; } = new();

        public List<Album> Albums { get; set; } = new();
    }
}