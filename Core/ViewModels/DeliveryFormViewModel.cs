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

        [MaxLength(14, ErrorMessage =Errors.MaxLength)]
        public string NID { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = Errors.MaxLength, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password), Display(Name = "Confirm Password")]

        [Compare("Password", ErrorMessage = Errors.ConfirmPasswordMatch)]
        public string ConfirmPassword { get; set; } = null!;

        #endregion

        #region Relations
        public string AppUserId { get; set; } = null!;
        #endregion
    }
}
