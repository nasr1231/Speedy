using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels
{
    public class PaymentFormViewModel
    {
        public string Id { get; set; } = null!;

        [Required(ErrorMessage ="من فضلك اختار وسيلة دفع")]
        public string Name { get; set; } = null!;
        public string? handle { get; set; }
        public string? PhoneNumber { get; set; }
        public string? HolderName { get; set; } 
    }
}
