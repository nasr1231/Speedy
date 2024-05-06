namespace Speedy.Core.ViewModels
{
    public class BaseViewModel
    {
        public AppUser? CreatedBy { get; set; }
        public string CreatedById { get; set; } = null!;
        public DateTime CreatedOn { get; set; } 

        public bool IsActive { get; set; }
        
        public string? LastUpdatedById { get; set; }
        public DateTime? LastUpdatedOn { get; set; }        
    }
}
