using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Core.Consts;
using Speedy.Services.User;

namespace Speedy.Controllers
{
    public class IndividualsController(ApplicationDbContext context, IMapper mapper, IUserService userService) : Controller
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
            return View("IndividualForm", InitialIndividualForm());
        }

        [HttpPost]
        public async Task<IActionResult> Create(IndividualFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

			var userForm = new UserFormViewModel
			{
				Password = model.Password,
				Email = model.Email,
				ConfirmPassword = model.ConfirmPassword,
				CreatedOn = model.CreatedOn,
				IsActive = false,
				SelectedRoles = AppRoles.Individual
			};

			var result = await _userService.SubmitUser(userForm);

            if (!result.IsSuccess)
                return View(model);

            var individual = new Individual
            {
                AppUserId = result.UserId!,
                CreatedOn = model.CreatedOn,                
                referralCode = model.ReferralCode,                
            };

            _context.Add(individual);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
        private IndividualFormViewModel InitialIndividualForm(IndividualFormViewModel? model = null)
        {
            IndividualFormViewModel individualFormView = model ?? new IndividualFormViewModel();
            
            var citiesTask = _context.Cities.Where(c => !c.IsDeleted).OrderBy(c => c.Name).ToList();
            var governoratesTask = _context.Governorates.Where(c => !c.IsDeleted).OrderBy(c => c.Name).ToList();

            individualFormView.Cities = _mapper.Map<IEnumerable<SelectListItem>>(citiesTask);
            individualFormView.Governorates = _mapper.Map<IEnumerable<SelectListItem>>(governoratesTask);

            return individualFormView;
        }
    }
}
