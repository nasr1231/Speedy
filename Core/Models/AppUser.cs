using Microsoft.AspNetCore.Identity;

namespace Speedy.Core.Models
{
    public class AppUser : IdentityUser
    {        
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string? ProfilePictureIUrl { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }

    }
}
