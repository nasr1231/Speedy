using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Core.Consts;
using Speedy.Core.ViewModels;
using Speedy.Services.User;

namespace Speedy.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class IndividualsController(ApplicationDbContext context, IMapper mapper, IUserService userService, IDataService dataService) : Controller
	{
		private readonly ApplicationDbContext _context = context;
		private readonly IMapper _mapper = mapper;
		private readonly IUserService _userService = userService;
        private readonly IDataService _dataService = dataService;

        public async Task<IActionResult> Index()
		{
            var individuals = await _dataService.GetAllIndividualsAsync();

            if (individuals == null)
                return NotFound();

            var individualsView = individuals.Select(d => new IndividualViewModel
            {
                Id = d.Id,
                NID = d.AppUser!.NID,
                CreatedOn = d.CreatedOn,
                Email = d.AppUser.Email,
                MobileNumber = d.AppUser.PhoneNumber,
                IsDeleted = d.IsDeleted,
                FirstName = d.AppUser.FirstName,
                LastName = d.AppUser.LastName,			
				IsActive = d.AppUser.IsActive
            });

            return View(individualsView);
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
				return View("IndividualForm", model);

			using var transaction = _context.Database.BeginTransaction();

			var userForm = new UserFormViewModel
			{
				Password = model.Password,
				Email = model.Email,
				ConfirmPassword = model.ConfirmPassword,
				SelectedRoles = AppRoles.Individual,
				FirstName = model.FirstName,
				LastName = model.LastName,
				NID = model.NID,
                PhoneNumber = model.MobileNumber
            };

			var result = await _userService.SubmitUser(userForm);

			if (!result.IsSuccess)
			{
				ModelState.AddModelError(string.Empty, result.Error!);
				return View("IndividualForm", InitialIndividualForm(model));
			}

			var individual = new Individual
			{
				AppUserId = result.UserId!,
				referralCode = model.ReferralCode,
			};

			_context.Add(individual);
			await _context.SaveChangesAsync();

			transaction.Commit();

            return RedirectToAction("Index", "Home");
		}
		private IndividualFormViewModel InitialIndividualForm(IndividualFormViewModel? model = null)
		{
			IndividualFormViewModel individualFormView = model ?? new IndividualFormViewModel();			
			var governoratesTask = _context.Governorates.Where(c => !c.IsDeleted).OrderBy(c => c.Name).ToList();

			individualFormView.Governorates = _mapper.Map<IEnumerable<SelectListItem>>(governoratesTask);

			return individualFormView;
		}
	}
}
