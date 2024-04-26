using Microsoft.AspNetCore.Identity;
using Speedy.Core.Consts;

namespace Speedy.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedAdminUser(UserManager<AppUser> userManager)
        {
            AppUser admin = new()
            {
                UserName = "admin",
                IsActive = true,
                FirstName = "Mohamed",
                LastName = "Nasr",
                Email = "admin@Speedy.com",
                EmailConfirmed = true,
                Gender = "Male",
                CreatedOn = DateTime.Now,
                Age = 22,
                NID = "00000000000000"
            };

            var user = await userManager.FindByEmailAsync(admin.Email);

            if(user is null)
            {
                await userManager.CreateAsync(admin, "Speedy@1234");
                await userManager.AddToRoleAsync(admin, AppRoles.Admin);
            }
        }
        
    }
}
