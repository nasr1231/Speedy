using JazanWatan.Web.Core.Consts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Speedy.Core.ViewModels
{
    public class ResetPasswordFormViewModel
    {
        public string Id { get; set; } = null!;        

        [DisplayName("كلمة المرور الحالية")]
        [Required(ErrorMessage = Errors.isRequired)]
        [StringLength(100, ErrorMessage = Errors.MaxLength, MinimumLength = 8)]
        [DataType(DataType.Password)]        
        public string OldPassword { get; set; } = null!;

        [DisplayName("كلمة المرور الجديدة")]
        [Required(ErrorMessage = "حقل كلمة المرور الجديدة مطلوب!")]
        [StringLength(100, ErrorMessage = Errors.MaxLength, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DisplayName("تأكيد كلمة المرور")]
        [Compare("Password", ErrorMessage = Errors.ConfirmPasswordMatch)]       
        [Required(ErrorMessage = Errors.isRequired)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
