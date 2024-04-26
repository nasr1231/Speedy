using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.Models
{
    public class AppUser : IdentityUser
    {
        #region Properties
        [MaxLength(40)]
        public string FirstName { get; set; } = null!;
        [MaxLength(40)]
        public string LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        [Range(16,80)]
        public string Age { get; set; } = null!;
        public string? ProfilePictureIUrl { get; set; }
        [MaxLength(20)]
        public string NID { get; set; } = null!;
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
