using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Speedy.Controllers
{
    public class ProfilesController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        public IActionResult IndividualProfile()
        {
            var user = _context.Individuals!
                .Include(i => i.AppUser)
                .Include(i => i.City)
                .SingleOrDefault(st => st.AppUserId == User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var orders = _context.Orders.Where(x=>x.AppUserId == User.FindFirst(ClaimTypes.NameIdentifier)!.Value).ToList();

            if (user is null)
                return NotFound("الحساب مش موجود والله ياريس");

            var userView = new IndividualProfileViewModel
            {
                Address = user.AppUser.Address,
                City = user.City.Name,
                Email = user.AppUser.Email,
                FirstName = user.AppUser.FirstName,
                LastName = user.AppUser.LastName,
                PhoneNumber = user.AppUser.PhoneNumber,
                Id = user.AppUserId,
                IsDeleted = user.IsDeleted,
                Orders = orders
            };

            return View(userView);
        }
        public async Task<IActionResult> StartUpProfile()
        {
            var user = await _context.StartUps!
                .Include(i => i.AppUser)
                .Include(i => i.City)
                .SingleOrDefaultAsync(st => st.AppUserId == User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            if (user is null)
                return NotFound("لماذا نصر بيه سمت مطرية");

            var userView = new StartUpProfileViewModel
            {
                CompanyName = user.StartUpName,
                IsDeleted = user.IsDeleted,
                City = user.City.Name,
                Address = user.Address!,
                Email = user.AppUser.Email!,
                EstablishDate = user.FoundingDate,
                FirstName = user.AppUser.FirstName,
                LastName = user.AppUser.LastName,
                Id = user.Id,
                Urls = user.Url,
                PhoneNumber = user.AppUser.PhoneNumber!
            };

            return View(userView);
        }
    }
}
