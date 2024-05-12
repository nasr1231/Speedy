using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Core.Consts;
using Speedy.Services.User;

namespace Speedy.Controllers
{
    public class StartUpsController(ApplicationDbContext context, IMapper mapper, IUserService userService) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IUserService _userService = userService;
        public IActionResult Index()
        {
            return View();
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
				SelectedRoles = AppRoles.StartUp
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
                CreatedOn = DateTime.Now,
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
