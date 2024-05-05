using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Core.Consts;
using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels.RelatedData
{
    public class CityFormViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Display(Name ="أسم المدينة")]
        [Required(ErrorMessage = Errors.isRequired)]
        [MaxLength(40, ErrorMessage = Errors.MaxLength)]
        public string Name { get; set; } = null!;

        [Display(Name = "المحافظة")]
        [Required(ErrorMessage = Errors.isRequired)]
        public int GovernorateId { get; set; }
        public IEnumerable<SelectListItem> Governorate { get; set; } = null!;        
    }
}
