using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Core.Consts;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels
{
    public class UserRoleFormViewModel
    {
        [DisplayName("دور المستخدم")]
        [Required(ErrorMessage = Errors.isRequired)]
        public string SelectedRoles { get; set; } = null!;
        public IEnumerable<SelectListItem>? Roles { get; set; }
    }
}
