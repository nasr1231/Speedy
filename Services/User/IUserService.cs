namespace Speedy.Services.User;

public interface IUserService
{
    public Task<(bool IsSuccess, AppUser? AppUser, string? Error)> SubmitUser(UserFormViewModel userForm);
}
