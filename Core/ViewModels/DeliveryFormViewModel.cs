using Speedy.Core.Consts;
using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels
{
    public class DeliveryFormViewModel : BaseViewModel
    {
        #region Properties
        [Required(ErrorMessage = Errors.isRequired)]
        [MaxLength(11, ErrorMessage = Errors.MaxLength)]
        public string MobileNumber { get; set; } = null!;

        public bool HasWhatsApp { get; set; }

        #endregion

        #region Relations
        public string AppUserId { get; set; } = null!;
        #endregion
    }
}
