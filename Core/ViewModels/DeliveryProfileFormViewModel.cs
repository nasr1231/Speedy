
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class DeliveryProfileFormViewModel
{
    #region Properties
    public string Id { get; set; } = null!;

    [Display(Name = "رقم الهاتف المحمول")]
    [Required(ErrorMessage = Errors.isRequired)]
    [MaxLength(11, ErrorMessage = Errors.MaxLength)]
    public string MobileNumber { get; set; } = null!; 

    #endregion

    #region Relations && Overloads        

    [DisplayName("مناطق العمل")]
    [Required(ErrorMessage = Errors.isRequired)]
    public List<int> SelectedAreasId { get; set; } = [];
    public IEnumerable<SelectListItem>? ServiceArea { get; set; }
    #endregion
}