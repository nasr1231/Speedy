using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels.RelatedData
{
    public class ServiceAreaFormViewModel
    {
        public int Id { get; set; }

        [Display(Name = "اسم المنطقة")]
        [Required(ErrorMessage = Errors.isRequired)]
        [MaxLength(40, ErrorMessage = Errors.MaxLength)]
        public string Name { get; set; } = null!;
    }
}
