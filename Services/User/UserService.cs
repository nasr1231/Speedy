using Microsoft.AspNetCore.Identity;

namespace Speedy.Services.User;

public class UserService(UserManager<AppUser> userManager) : IUserService
{
    private readonly UserManager<AppUser> _userManager = userManager;

    public async Task<(bool IsSuccess, AppUser? AppUser, string? Error)> SubmitUser(UserFormViewModel userForm)
    {
        var user = new AppUser
        {
            Email = userForm.Email,           
            UserName = userForm.Email,
            IsActive = true,            
            EmailConfirmed = true,
            NID =  userForm.NID,   
            FirstName = userForm.FirstName,
            LastName = userForm.LastName,
            PhoneNumber = userForm.PhoneNumber,
            Address = userForm.Address,
            BirthDate = userForm.BirthDate,
            Gender = userForm.Gender
        };

        var createUserResult = await _userManager.CreateAsync(user, userForm.Password);

        if (!createUserResult.Succeeded)
            return (IsSuccess: false, AppUser: null, Error: string.Join(',', createUserResult.Errors.Select(e => e.Description)));

        var addToRoleResult = await _userManager.AddToRoleAsync(user, userForm.SelectedRoles);

        if (!addToRoleResult.Succeeded)
            return (IsSuccess: false, AppUser: null, Error: string.Join(',', addToRoleResult.Errors.Select(e => e.Description)));

        return (IsSuccess: true, AppUser: user, Error: null);
    }
}
