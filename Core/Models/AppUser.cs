using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(UserName), IsUnique = true)]
    public class AppUser : IdentityUser
    {
        #region Properties
        [MaxLength(40)]
        public string FirstName { get; set; } = null!;
        [MaxLength(40)]
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;

        [Range(16, 80)]
        public byte? Age { get; set; }
        public string? ProfilePictureIUrl { get; set; } = string.Empty;

        [MaxLength(20)]
        public string NID { get; set; } = string.Empty;
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
