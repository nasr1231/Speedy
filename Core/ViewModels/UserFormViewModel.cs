using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Core.Consts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels
{
    public class UserFormViewModel
    {
        #region Properties
        public int Id { get; set; }

        [MaxLength(60, ErrorMessage = Errors.MaxLength), Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;
        public string? Address { get; set; } 
        public string? Gender { get; set; } 
        public DateTime BirthDate{ get; set; } 

		[MaxLength(60, ErrorMessage = Errors.MaxLength), Display(Name = "Last Name")]
		public string LastName { get; set; } = null!;

		[MaxLength(60, ErrorMessage = Errors.MaxLength), Display(Name = "User Name")]
        public string UserName{ get; set; } = null!;

        [MaxLength(60, ErrorMessage = Errors.MaxLength), Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [MaxLength(11, ErrorMessage = Errors.MaxLength), Display(Name = "رقم الهاتف")]
        public string? PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = Errors.MaxLength, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password), Display(Name = "Confirm Password")]
        
        [Compare("Password", ErrorMessage = Errors.ConfirmPasswordMatch)]
        public string ConfirmPassword { get; set; } = null!;

        public string? ProfilePictureUrl { get; set; }

        public string? NID { get; set; }
        #endregion

        #region Roles
        [Display(Name = "Roles")]
        public string SelectedRoles { get; set; } = null!;
        public IEnumerable<SelectListItem>? Roles { get; set; }
        #endregion
    }
}
