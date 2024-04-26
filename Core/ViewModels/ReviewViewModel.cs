using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels
{
    public class ReviewViewModel : BaseModel
    {
        public int Rate { get; set; }
        public string? Description { get; set; } = string.Empty;
    }
}
