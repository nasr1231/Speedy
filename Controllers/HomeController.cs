using Speedy.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;
using System.Data;
using Speedy.Core.Consts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Speedy.Filters;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

		public async Task <IActionResult> InitialCreate()
		{
            IQueryable<IdentityRole> rolesQueryable = _roleManager.Roles;

            rolesQueryable = rolesQueryable.Where(r => r.Name != AppRoles.Admin && r.Name != AppRoles.Clerk);
            
			var rolesList = await rolesQueryable
           .Select(r => new SelectListItem { Text = r.Name, Value = r.Name }).ToListAsync();

            var rolesView = new UserRoleFormViewModel { Roles = rolesList };

            return PartialView("~/Views/Users/_InitialCreateForm.cshtml", rolesView);
		}
        
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> Create(UserRoleFormViewModel model)
        {
            var userForm = new UserFormViewModel { SelectedRoles = model.SelectedRoles};
            
                //return View("~/Views/Users/DeliveryForm.cshtml", userForm);

            if (model.SelectedRoles == AppRoles.Delivery)
                return RedirectToAction("CompleteDoctorProfileInfo", "Home", new { userId = user.Id });

            return BadRequest();
        }

        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> Create(UserFormViewModel UserForm)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if ((UserForm.SelectedRoles ?? AppRoles.Individual) == AppRoles.Delivery)
            {
				Delivery deliveryUser = new()
				{

				};
			}

            var user = new AppUser
            {
                Email = UserForm.Email,
                NormalizedEmail = UserForm.Email.ToUpper(),                
                UserName = UserForm.Email,
				IsActive = UserForm.IsActive == true,
                NormalizedUserName = UserForm.Email.ToUpper(),
                EmailConfirmed = true,                               
            };                 

            var UserResult = await _userManager.CreateAsync(user, UserForm.Password);

            if (!UserResult.Succeeded)
                return BadRequest(string.Join(',', UserResult.Errors.Select(e => e.Description)));

            //await _userManager.AddToRolesAsync(user, model.selectedRoles);
            

            return PartialView("_NewRow");
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
