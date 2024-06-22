using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Speedy.Core.ViewModels
{
    public class CitiesHomeViewModel
    {
        [Display(Name = "موقعك")]
        [Required(ErrorMessage =Errors.isRequired)]
        public int CityId { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; } = [];
    }
}
