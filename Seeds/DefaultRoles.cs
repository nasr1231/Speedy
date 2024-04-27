using Microsoft.AspNetCore.Identity;
using Speedy.Core.Consts;

namespace Speedy.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));
                await roleManager.CreateAsync(new IdentityRole(AppRoles.StartUp));
                await roleManager.CreateAsync(new IdentityRole(AppRoles.Individual));
                await roleManager.CreateAsync(new IdentityRole(AppRoles.Delivery));
                await roleManager.CreateAsync(new IdentityRole(AppRoles.Clerk));
            }
        }
    }
}
