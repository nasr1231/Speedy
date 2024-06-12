using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;

namespace Speedy.Core.ViewModels
{
    public class CitiesHomeViewModel
    {
        [Required]
        public int CityId { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; } = [];
    }
}
