using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using Speedy.Core.Consts;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels
{
    public class IndividualFormViewModel
    {
        #region Properties
        [Display(Name = "الأسم الأول")]
        [Required(ErrorMessage = Errors.isRequired)]
        [MaxLength(40, ErrorMessage = Errors.MaxLength)]
        public string FirstName { get; set; } = null!;

        [Display(Name = "الأسم الأخير")]
        [Required(ErrorMessage = Errors.isRequired)]
        [MaxLength(40, ErrorMessage = Errors.MaxLength)]
        public string LastName { get; set; } = null!;

        [Display(Name = "البريد الالكتروني")]
        [EmailAddress]
        [Required(ErrorMessage = Errors.isRequired)]
        [MaxLength(40, ErrorMessage = Errors.MaxLength)]
        public string Email { get; set; } = null!;

        [Display(Name = "الرقم القومي")]
        [MaxLength(14, ErrorMessage = Errors.MaxLength)]
        [Required(ErrorMessage = Errors.isRequired)]
        public string NID { get; set; } = string.Empty;

        [Display(Name = "رقم الهاتف المحمول")]
        [Required(ErrorMessage = Errors.isRequired)]
        [MaxLength(11, ErrorMessage = Errors.MaxLength)]
        public string MobileNumber { get; set; } = null!;

        [Display(Name = "العنوان")]
        [Required(ErrorMessage = Errors.isRequired)]
        [MaxLength(100, ErrorMessage = Errors.MaxLength)]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = Errors.isRequired)]
        [MaxLength(5, ErrorMessage = "يا اما ذكر يا اما أنثى")]
        [Display(Name = "النوع")]
        public string Gender { get; set; } = null!;

        [Display(Name = "تاريخ الميلاد")]
        [Required(ErrorMessage = Errors.isRequired)]
        public DateTime BirthDate { get; set; } = DateTime.Now.AddYears(-16);

        public bool HasWhatsApp { get; set; }


        [Display(Name = "الصورة الشخصية")]
        [Required(ErrorMessage = Errors.isRequired)]
        public IFormFile UserImage { get; set; } = null!;


        [Required(ErrorMessage = Errors.isRequired)]
        [StringLength(100, ErrorMessage = Errors.MaxLength, MinimumLength = 6)]
        [DataType(DataType.Password), Display(Name = "كلمة المرور")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = Errors.isRequired)]
        [DataType(DataType.Password), Display(Name = "تأكيد كلمة المرور")]
        [Compare("Password", ErrorMessage = Errors.ConfirmPasswordMatch)]
        public string ConfirmPassword { get; set; } = null!;
        [StringLength(12, ErrorMessage = "كود دعوة خاطئ")]
        public string? ReferralCode { get; set; }
        #endregion

        #region Relations && Overloads        

        [DisplayName("المدينة")]
        [Required(ErrorMessage = Errors.isRequired)]
        public int SelectedCityId { get; set; }
        public IEnumerable<SelectListItem>? Cities { get; set; }

        [DisplayName("المحافظة")]
        [Required(ErrorMessage = Errors.isRequired)]
        public int SelectedGovernorateId { get; set; } 
        public IEnumerable<SelectListItem>? Governorates { get; set; }
        #endregion
    }
}
