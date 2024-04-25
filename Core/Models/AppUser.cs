using Microsoft.AspNetCore.Identity;

namespace Speedy.Core.Models
{
    public class AppUser : IdentityUser
    {
        #region Properties
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;        
        public string Age { get; set; } = null!;
        public string? ProfilePictureIUrl { get; set; }        
        public double NID { get; set; }
        #endregion

        #region Conditions
        public bool IsActive { get; set; }
        public string? CreatedById { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now.ToUniversalTime();

        public string? LastUpdatedById { get; set; }
        public DateTime? LastUpdatedOn { get; set; }                
        #endregion

        #region Relations
        public Delivery? Delivery { get; set; }        
        #endregion
    }
}
