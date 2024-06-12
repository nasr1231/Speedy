using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Core.Consts;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace Speedy.Core.ViewModels
{
    public class DeliveryFormViewModel
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

        //[Remote("IsUnique", "Deliveries", ErrorMessage = Errors.isUnique)]
		[Display(Name = "البريد الإلكتروني")]
        [EmailAddress]
		[Required(ErrorMessage = Errors.isRequired)]
		[MaxLength(40, ErrorMessage = Errors.MaxLength)]
		public string Email { get; set; } = null!;

		[Display(Name = "رقم الهاتف المحمول")]
		[Required(ErrorMessage = Errors.isRequired)]
        [MaxLength(11, ErrorMessage = Errors.MaxLength)]
        public string MobileNumber { get; set; } = null!;

		[Display(Name = "العنوان")]
		[Required(ErrorMessage = Errors.isRequired)]
		[MaxLength(100, ErrorMessage = Errors.MaxLength)]
		public string Address { get; set; } = null!;        
		public bool HasWhatsApp { get; set; }

        [Display(Name = "الرقم القومي")]
        [MaxLength(14, ErrorMessage =Errors.MaxLength)]
        [Required(ErrorMessage =Errors.isRequired)]
        public string NID { get; set; } = string.Empty;

        [Required(ErrorMessage = Errors.isRequired)]
        [StringLength(100, ErrorMessage = Errors.MaxLength, MinimumLength = 6)]
        [DataType(DataType.Password), Display(Name = "كلمة المرور")]
        public string Password { get; set; } = null!;

		[DataType(DataType.Password), Display(Name = "تأكيد كلمة المرور")]
		[Compare("Password", ErrorMessage = Errors.ConfirmPasswordMatch)]
		public string ConfirmPassword { get; set; } = null!;

		[Display(Name = "تاريخ الميلاد")]
		[Required(ErrorMessage = Errors.isRequired)]        
        public DateTime BirthDate { get; set; } = DateTime.Now.AddYears(-16);

		[Required(ErrorMessage = Errors.isRequired)]
        [Display(Name = "النوع")]
        [MaxLength(5, ErrorMessage = "يا اما ذكر يا اما أنثى")]
		public string Gender { get; set; } = null!;

		[Required(ErrorMessage = Errors.isRequired)]
        public List<IFormFile> Attachments { get; set; } = [];

        #endregion

        #region Relations && Overloads        

		[DisplayName("نوع وسيلة التوصيل التي تملكها")]
		[Required(ErrorMessage = Errors.isRequired)]
		public int SelectedShippingMethod { get; set; }
		public IEnumerable<SelectListItem>? ShippingMethods { get; set; }

        [DisplayName("المدينة")]
        [Required(ErrorMessage = Errors.isRequired)]
        public int SelectedCityId { get; set; }
        public IEnumerable<SelectListItem>? Cities { get; set; }

        [DisplayName("المحافظة")]
        [Required(ErrorMessage = Errors.isRequired)]
        public int SelectedGovernorate { get; set; }
        public IEnumerable<SelectListItem>? Governorates { get; set; }

        [DisplayName("مناطق العمل")]
        [Required(ErrorMessage = Errors.isRequired)]
        public List<string>? ServiceArea { get; set; } = [];
        #endregion
    }
}
