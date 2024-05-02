namespace Speedy.Services.User;

public interface IUserService
{
    public Task<(bool IsSuccess, string? UserId, string? Error)> SubmitUser(UserFormViewModel userForm);
}
