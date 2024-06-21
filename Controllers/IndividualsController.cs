using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Core.Consts;
using Speedy.Core.Models;
using Speedy.Core.ViewModels;
using Speedy.Services.Individuals;
using Speedy.Services.User;

namespace Speedy.Controllers
{
    public class IndividualsController(UserManager<AppUser> userManager,ApplicationDbContext context, IMapper mapper, IUserService userService, IDataService dataService, IIndividualService individualService, IAttachmentService attachmentService) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IUserService _userService = userService;
        private readonly IIndividualService _individualService = individualService;
        private readonly IDataService _dataService = dataService;
        private readonly IAttachmentService _attachmentService = attachmentService;
        private readonly UserManager<AppUser> _userManager = userManager;

        public async Task<IActionResult> Index()
        {
            var individuals = await _dataService.GetAllIndividualsAsync();

            if (individuals == null)
                return NotFound();

            var individualsView = individuals.Select(d => new IndividualViewModel
            {
                Id = d.Id,
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
        public IActionResult Create()
        {
            return View("IndividualForm", InitialIndividualForm());
        }

        [HttpGet]
        public IActionResult Profile(string id)
        {
            var user = _context.Individuals
               .Include(c => c.City)
               .Include(ap => ap.AppUser)
               .SingleOrDefault(st => st.AppUserId == id);

            if (user is null)
                return NotFound();

            var userView = new IndividualProfileViewModel
            {
                Address = user.AppUser!.Address,
                City = user.City.Name,
                Email = user.AppUser.Email,
                FirstName = user.AppUser!.FirstName,
                LastName = user.AppUser!.LastName,
                PhoneNumber = user.AppUser.PhoneNumber,
                Id = user.AppUserId,
                IsDeleted = user.IsDeleted
            };

            return View("Profile", userView);
        }

        [HttpPost]
        public async Task<IActionResult> Create(IndividualFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("IndividualForm", InitialIndividualForm(model));

            using var transaction = _context.Database.BeginTransaction();

            var userForm = new UserFormViewModel
            {
                Password = model.Password,
                Email = model.Email,
                ConfirmPassword = model.ConfirmPassword,
                SelectedRoles = AppRoles.Individual,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.MobileNumber,
                Address = model.Address,
                BirthDate = model.BirthDate,
                Gender = model.Gender
            };

            var result = await _userService.SubmitUser(userForm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                return View("IndividualForm", InitialIndividualForm(model));
            }

            var individual = new Individual
            {
                AppUserId = result.AppUser!.Id,
                referralCode = model.ReferralCode,
                CityId = model.SelectedCityId,
            };

            var imageAttachment = await _attachmentService.UploadImageAsync(
              attachedFile: model.UserImage,
            entityName: "Individual",
             userName: individual.AppUserId);

            result.AppUser.ProfilePictureIUrl = imageAttachment.AttachmentUrl!;

            await _userManager.UpdateAsync(result.AppUser);

            _context.Add(individual);
            await _context.SaveChangesAsync();

            transaction.Commit();

            return RedirectToPage("/Account/Login", new { area = "Identity" });
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
