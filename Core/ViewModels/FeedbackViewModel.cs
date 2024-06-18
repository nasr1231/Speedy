using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels
{
    public class FeedbackViewModel
    {
        public string UserId { get; set; } = null!;

        [Display(Name ="الرسالة")]
        [Required(ErrorMessage = Errors.isRequired)]
        public string Message { get; set; } = null!;
        public string? UserName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
