using Azure.Identity;
using Speedy.Core.Consts;
using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels
{
	public class LoginViewModel
	{
		[Required]		
		[Display(Name = "Username Or Email")]
		[MaxLength(100, ErrorMessage = Errors.MaxLength)]
		public string Username { get; set; } = null!;

		[Required]
		[DataType(DataType.Password)]		
		public string Password { get; set; } = null!;
		
		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }
	}
}
