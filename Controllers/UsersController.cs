using AutoMapper;
using Bookify.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Core.Consts;
using System.Security.Claims;

namespace Speedy.Controllers
{   
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var ViewModel = _mapper.Map<IEnumerable<UserViewModel>>(users);
            return View(ViewModel);
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

            AppUser user = new() { 
                FirstName = model.FullName,
                UserName = model.UserName,
                Email = model.Email,
                CreatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value                
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, model.SelectedRoles);
                var viewModel = _mapper.Map<UserViewModel>(model);
                return PartialView("_UserRow", viewModel);
            }

            return BadRequest();
        }
    }
}
