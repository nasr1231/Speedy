using Speedy.Core.Consts;
using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels.RelatedData
{
    public class ShippingMethodFormViewModel : BaseModel
    {
        public int Id { get; set; }

        [Display(Name = "وسيلة الشحن")]
        [Required(ErrorMessage = Errors.isRequired)]
        [MaxLength(40, ErrorMessage = Errors.MaxLength)]
        public string Name { get; set; } = null!;
    }
}
