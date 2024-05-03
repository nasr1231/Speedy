using Speedy.Core.Consts;
using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels
{
    public class IndividualFormViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Errors.isRequired)]
        [MaxLength(40, ErrorMessage = Errors.MaxLength)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = Errors.isRequired)]
        [MaxLength(40, ErrorMessage = Errors.MaxLength)]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = Errors.isRequired)]
        [MaxLength(100, ErrorMessage = Errors.MaxLength)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = Errors.isRequired)]
        [Range(15,80, ErrorMessage = "ربنا يديك طولة العمر بس  دخل العمر صح بالله عليك")]
        [EmailAddress]
        public byte Age { get; set; }

        [Required(ErrorMessage = Errors.isRequired)]
        [MaxLength(11, ErrorMessage = Errors.MaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [MaxLength(120, ErrorMessage = Errors.MaxLength)]
        [Required(ErrorMessage = Errors.isRequired)]
        public string Address { get; set; } = null!;
        [StringLength(12, ErrorMessage = "Invalid Referral Code")]
        public string? ReferralCode { get; set; }

       [Required]
        [StringLength(100, ErrorMessage = Errors.MaxLength, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password), Display(Name = "Confirm Password")]
        
        [Compare("Password", ErrorMessage = Errors.ConfirmPasswordMatch)]
        public string ConfirmPassword { get; set; } = null!;

        #region Relations
        public string AppUserId { get; set; } = null!;
        #endregion
    }
}
