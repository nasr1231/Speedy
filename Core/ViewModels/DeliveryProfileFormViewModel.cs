
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class DeliveryProfileFormViewModel
{
    #region Properties
    public int Id { get; set; }

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

    [Required(ErrorMessage = Errors.isRequired)]
    [StringLength(100, ErrorMessage = Errors.MaxLength, MinimumLength = 6)]
    [DataType(DataType.Password), Display(Name = "كلمة المرور")]
    public string Password { get; set; } = null!;

    #endregion

    #region Relations && Overloads        

    [DisplayName("المدينة")]
    [Required(ErrorMessage = Errors.isRequired)]
    public int SelectedCityId { get; set; }
    public IEnumerable<SelectListItem>? Cities { get; set; }    

    [DisplayName("مناطق العمل")]
    [Required(ErrorMessage = Errors.isRequired)]
    public List<string>? ServiceArea { get; set; } = [];
    #endregion
}