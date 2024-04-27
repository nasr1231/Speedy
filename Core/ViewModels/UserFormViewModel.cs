using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Core.Consts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels
{
    public class UserFormViewModel : BaseViewModel
    {
        #region Properties
        public int Id { get; set; }
        [MaxLength(60, ErrorMessage = Errors.MaxLength), Display(Name = "Full Name")]
        public string FullName { get; set; } = null!;
        [MaxLength(60, ErrorMessage = Errors.MaxLength), Display(Name = "User Name")]
        public string UserName{ get; set; } = null!;
        [MaxLength(60, ErrorMessage = Errors.MaxLength), Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(100, ErrorMessage = Errors.MaxLength, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password), Display(Name = "Confirm Password")]
        
        [Compare("Password", ErrorMessage = Errors.ConfirmPasswordMatch)]
        public string ConfirmPassword { get; set; } = null!;
        #endregion

        #region Roles
        [Display(Name = "Roles")]
        public IList<string> SelectedRoles { get; set; } = new List<string>();
        public IEnumerable<SelectListItem>? Roles { get; set; }
        #endregion
    }
}
