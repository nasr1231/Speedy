using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace Speedy.Core.ViewModels
{
    public class StartUpFormViewModel
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

		[Required(ErrorMessage = Errors.isRequired)]
		[StringLength(100, ErrorMessage = Errors.MaxLength, MinimumLength = 6)]
		[DataType(DataType.Password), Display(Name = "كلمة المرور")]
		public string Password { get; set; } = null!;

		[Required(ErrorMessage = Errors.isRequired)]	
		[DataType(DataType.Password), Display(Name = "تأكيد كلمة المرور")]
		[Compare("Password", ErrorMessage = Errors.ConfirmPasswordMatch)]
		public string ConfirmPassword { get; set; } = null!;

		[Display(Name = "رقم الهاتف المحمول")]
		[Required(ErrorMessage = Errors.isRequired)]
		[MaxLength(11, ErrorMessage = Errors.MaxLength)]
		public string MobileNumber { get; set; } = null!;

        [RequiredIf("IsOnline == true")]
		[Display(Name = "العنوان")]
		[Required(ErrorMessage = Errors.isRequired)]
		[MaxLength(100, ErrorMessage = Errors.MaxLength)]
		public string Address { get; set; } = null!;

		[Display(Name = "اسم الشركة")]
		[Required(ErrorMessage = Errors.isRequired)]
		[MaxLength(50, ErrorMessage = Errors.MaxLength)]
		public string StartUpName { get; set; } = null!;

		[AssertThat("EstablishDate <= Today()", ErrorMessage = "انت عامل الشركة شكك طيب ولا ايه")]
		[Display(Name = "تاريخ إنشاء الشركة")]
		[Required(ErrorMessage = Errors.isRequired)]
		public DateTime EstablishDate { get; set; } = DateTime.Now;

		public bool HasWhatsApp { get; set; }
		public List<string>? Urls { get; set; }
		public bool IsOnline { get; set; }				
		#endregion


		#region Relations && Overloads
		public string AppUserId { get; set; } = null!;		

		[DisplayName("المدينة")]
		[Required(ErrorMessage = Errors.isRequired)]
		public int SelectedCityId { get; set; }
		public IEnumerable<SelectListItem>? Cities { get; set; }

		[DisplayName("المحافظة")]
		[Required(ErrorMessage = Errors.isRequired)]
		public string SelectedGovernorateId { get; set; } = null!;
		public IEnumerable<SelectListItem>? Governorates { get; set; }
		#endregion
	}
}
