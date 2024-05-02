using Speedy.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.VisualBasic.FileIO;

namespace Speedy.Controllers
{
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
            var ViewModel = new UserFormViewModel {
                Roles = await _roleManager.Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync() 
            };

            return PartialView("_Form", ViewModel);
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
            };

            if (user == null)
                throw new ArgumentException("احا الموديل فاضي يابرنس");

            var result = await _userManager.CreateAsync(user, model.Password ?? throw new ArgumentException("Password cannot be null"));

            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, model.SelectedRoles);

                var viewModel = _mapper.Map<UserViewModel>(model);
                return PartialView("_RowData", viewModel);
            }            

            return BadRequest();
        }		
    }
}
