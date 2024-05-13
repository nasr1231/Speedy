using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Core.Consts;
using Speedy.Services.User;

namespace Speedy.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class StartUpsController(ApplicationDbContext context, IMapper mapper, IUserService userService, IDataService dataService) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IUserService _userService = userService;
        private readonly IDataService _dataService = dataService;
        public async Task<IActionResult> Index()
        {
            var startUps = await _dataService.GetAllStartUpsAsync();

            if (startUps == null)
                return NotFound();

            var startUpsView = startUps.Select(d => new StartUpViewModel
            {
                Id = d.Id,
                NID = d.AppUser!.NID,
                CreatedOn = d.CreatedOn,
                Email = d.AppUser.Email,
                MobileNumber = d.AppUser.PhoneNumber,
                IsDeleted = d.IsDeleted,
                FirstName = d.AppUser.FirstName,
                LastName = d.AppUser.LastName,
                IsActive = d.AppUser.IsActive,
                StartUpName = d.StartUpName,
                IsOnline = d.IsOnline
            });

            return View(startUpsView);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {            
            return View("StartUpForm", InitialStartUpForm());
        }

        [HttpPost]
        public async Task<IActionResult> Create(StartUpFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("StartUpForm", model);

            var userForm = new UserFormViewModel
            {
				Password = model.Password,
				Email = model.Email,
				ConfirmPassword = model.ConfirmPassword,	
				SelectedRoles = AppRoles.StartUp,
                PhoneNumber = model.MobileNumber
            };

            var result = await _userService.SubmitUser(userForm);

            if (!result.IsSuccess)
            {
				ModelState.AddModelError(string.Empty, result.Error!);
				return View("StartUpForm", InitialStartUpForm(model));
			}

			var startUp = new StartUp
            {
                AppUserId = result.UserId!,
                Address = model.Address,                
                FoundingDate = model.EstablishDate,
                IsOnline = model.IsOnline,
                StartUpName = model.StartUpName,                
                Url = model.Urls,                
            };

            _context.Add(startUp);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

		private StartUpFormViewModel InitialStartUpForm(StartUpFormViewModel? model = null)
		{
			StartUpFormViewModel startupFormView = model ?? new StartUpFormViewModel();
			
			var governoratesTask = _context.Governorates.Where(c => !c.IsDeleted).OrderBy(c => c.Name).ToList();
			
			startupFormView.Governorates = _mapper.Map<IEnumerable<SelectListItem>>(governoratesTask);

			return startupFormView;
		}
	}
}
