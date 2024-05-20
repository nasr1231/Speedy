using Microsoft.AspNetCore.Mvc.Rendering;

namespace Speedy.Core.ViewModels
{
    public class PaymentFormViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? handle { get; set; } = null!;
        public string? PhoneNumber { get; set; } = null!;
        public string? HolderName { get; set; } = null!;

        #region Relationships
        public int PaymentMethodId { get; set; }
        public IEnumerable<SelectListItem>? PaymentMethods { get; set; }
        #endregion
    }
}
