using Speedy.Core.Consts;
using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels.RelatedData
{
    public class GovernorateFormViewModel : BaseModel
    {
        public int Id { get; set; }

        [Display(Name = "المحافظة")]
        [Required(ErrorMessage = Errors.isRequired)]
        [MaxLength(40, ErrorMessage = Errors.MaxLength)]
        public string Name { get; set; } = null!;
    }
}
