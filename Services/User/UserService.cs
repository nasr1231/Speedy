using Microsoft.AspNetCore.Identity;

namespace Speedy.Services.User;

public class UserService(UserManager<AppUser> userManager) : IUserService
{
    private readonly UserManager<AppUser> _userManager = userManager;

    public async Task<(bool IsSuccess, string? UserId, string? Error)> SubmitUser(UserFormViewModel userForm)
    {
        var user = new AppUser
        {
            Email = userForm.Email,
            NormalizedEmail = userForm.Email.ToUpper(),
            UserName = userForm.Email,
            IsActive = userForm.IsActive == true,
            NormalizedUserName = userForm.Email.ToUpper(),
            EmailConfirmed = true,
        };

        var createUserResult = await _userManager.CreateAsync(user, userForm.Password);

        if (!createUserResult.Succeeded)
            return (IsSuccess: false, UserId: null, Error: string.Join(',', createUserResult.Errors.Select(e => e.Description)));

        var addToRoleResult = await _userManager.AddToRoleAsync(user, userForm.SelectedRoles);

        if (!addToRoleResult.Succeeded)
            return (IsSuccess: false, UserId: null, Error: string.Join(',', addToRoleResult.Errors.Select(e => e.Description)));

        return (IsSuccess: true, UserId: user.Id, Error: null);
    }
}
