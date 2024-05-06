using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Core.Consts;
using Speedy.Filters;

namespace Speedy.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class UsersController(ApplicationDbContext context, IMapper mapper, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, ILogger<LoginModel> logger) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<LoginModel> _logger = logger;


        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var ViewModel = _mapper.Map<IEnumerable<UserViewModel>>(users);
            return View("Index", ViewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> Create()
        {

            IQueryable<IdentityRole> rolesQueryable = _roleManager.Roles;

            rolesQueryable = rolesQueryable.Where(r => r.Name != AppRoles.Delivery && r.Name != AppRoles.StartUp && r.Name != AppRoles.Individual);

            var rolesList = await rolesQueryable
           .Select(r => new SelectListItem { Text = r.Name, Value = r.Name }).ToListAsync();

            var rolesView = new UserFormViewModel { Roles = rolesList };

            return PartialView("_Form", rolesView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserFormViewModel model)
        {
            if (ModelState.IsValid)
                return BadRequest();

            AppUser user = new()
            {
                FirstName = model.FullName,
                UserName = model.Email,
                NormalizedUserName = model.Email.ToUpper(),
                NormalizedEmail = model.Email.ToUpper(),
                Email = model.Email,
                EmailConfirmed = true,
                IsActive = true,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(ToCustomErrorString(result));

            await _userManager.AddToRoleAsync(user, model.SelectedRoles);

            var viewModel = _mapper.Map<UserViewModel>(model);

            return PartialView("_NewRow", viewModel);
        }

        private static string ToCustomErrorString(IdentityResult result)
        {
            var error = string.Empty;

            foreach (var identityError in result.Errors)
                error += $"{identityError.Description},";

            return error;
        }
    }
}
