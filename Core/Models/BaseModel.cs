using System.Reflection.Metadata.Ecma335;

namespace Speedy.Core.Models
{
    public class BaseModel
    {
        public AppUser? CreatedBy { get; set; }
        public string CreatedById { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.Now.ToUniversalTime();

        public AppUser? LastUpdatedBy { get; set; }
        public string? LastUpdatedById { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
