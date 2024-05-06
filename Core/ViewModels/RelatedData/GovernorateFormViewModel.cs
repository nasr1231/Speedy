using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels.RelatedData
{
    public class GovernorateFormViewModel : BaseModel
    {        
        public int Id { get; set; }

        [Display(Name = "Governorate Name")]
        [Required(ErrorMessage = Errors.isRequired)]
        [MaxLength(40, ErrorMessage = Errors.MaxLength)]
        public string Name { get; set; } = null!;
    }
}
