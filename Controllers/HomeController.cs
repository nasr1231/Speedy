using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Core.Consts;
using Speedy.Filters;

namespace Speedy.Controllers
{
    public class HomeController(ApplicationDbContext context, IMapper mapper, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, ILogger<LoginModel> logger) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<LoginModel> _logger = logger;

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            IQueryable<IdentityRole> rolesQueryable = _roleManager.Roles;

            rolesQueryable = rolesQueryable.Where(r => r.Name != AppRoles.Admin && r.Name != AppRoles.Clerk);

            var rolesList = await rolesQueryable
                .Select(r => new SelectListItem { Text = r.Name, Value = r.Name }).ToListAsync();

            var rolesView = new UserRoleFormViewModel { Roles = rolesList };

            return PartialView("~/Views/Users/InitialCreateForm.cshtml", rolesView);
        }

        [HttpPost]
        //[AjaxOnly]
        public async Task<IActionResult> Register(UserRoleFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Role!");

            if (model.SelectedRoles == AppRoles.Delivery)
                return RedirectToAction("Create", "Deliveries");

            if (model.SelectedRoles == AppRoles.Individual)
                return RedirectToAction("Create", "Individuals");
            if (model.SelectedRoles == AppRoles.StartUp)
                return RedirectToAction("Create", "StartUps");

            return BadRequest("Invalid Role!");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {

            returnUrl ??= Url.Content("~/");


            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var userName = model.Username.ToUpper();
                var user = await _userManager.Users
                    .SingleOrDefaultAsync(u => u.NormalizedUserName == userName || u.NormalizedEmail == userName && u.IsActive);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(ModelState);
        }
    }
}
