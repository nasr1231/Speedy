using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Speedy.Services.User;

public class UserService(ApplicationDbContext context,UserManager<AppUser> userManager) : IUserService
{
    private readonly ApplicationDbContext _context = context;
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
    public async Task<Individual?> GetIndividualAsync(string individualId)
    {
        IQueryable<Individual> individualQueryable = _context.Individuals!            
            .Include(s => s.AppUser)
            .Include(c => c.City)
            .ThenInclude(g => g.Governorate);

        individualQueryable = individualQueryable.AsNoTracking();

        var delivery = await individualQueryable.SingleOrDefaultAsync(d => d.AppUserId == individualId);

        return delivery;
    }

    public async Task<StartUp?> GetStartUpAsync(string startUpId)
    {
        IQueryable<StartUp> startUpQueryable = _context.StartUps
                .Include(ap => ap.AppUser)                
                .Include(c => c.City)
                 .ThenInclude(g => g.Governorate);

        startUpQueryable = startUpQueryable.AsNoTracking();

        var startUp = await startUpQueryable.SingleOrDefaultAsync(d => d.AppUserId == startUpId);

        return startUp;
    }


}
