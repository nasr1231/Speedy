using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Speedy.Core.Consts;
using Speedy.Core.Models;
using Speedy.Services.StartUps;
using Speedy.Services.User;

namespace Speedy.Controllers
{
    public class StartUpsController(ApplicationDbContext context, IMapper mapper, IStartUpService startService, IUserService userService, IDataService dataService, UserManager<AppUser> userManager, IAttachmentService attachmentService) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IUserService _userService = userService;
        private readonly IDataService _dataService = dataService;
        private readonly IAttachmentService _attachmentService = attachmentService;
        private readonly IStartUpService _startService = startService;
        private readonly UserManager<AppUser> _userManager = userManager;
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
        public IActionResult Create()
        {
            return View("StartUpForm", InitialStartUpForm());
        }        

        [HttpPost]
        public async Task<IActionResult> Create(StartUpFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("StartUpForm", model);

            var transaction = _context.Database.BeginTransaction();

            var userForm = new UserFormViewModel
            {
                Password = model.Password,
                Email = model.Email,
                ConfirmPassword = model.ConfirmPassword,
                SelectedRoles = AppRoles.StartUp,
                PhoneNumber = model.MobileNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
                NID = model.NID,
                Address = model.Address,                
            };

            var result = await _userService.SubmitUser(userForm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                return View("StartUpForm", InitialStartUpForm(model));
            }

            var startUp = new StartUp
            {
                AppUserId = result.AppUser!.Id,
                Address = model.Address,
                FoundingDate = model.EstablishDate,
                IsOnline = model.IsOnline,
                StartUpName = model.StartUpName,
                Url = model.Urls,
                CityId = model.SelectedCityId,
            };

            var imageAttachment = await _attachmentService.UploadImageAsync(
              attachedFile: model.UserImage,
              entityName: "StartUps",
             userName: startUp.AppUserId);

            if (!imageAttachment.isUploaded)
                return BadRequest(imageAttachment.errorMessage);

            result.AppUser.ProfilePictureIUrl = imageAttachment.AttachmentUrl!;

            await _userManager.UpdateAsync(result.AppUser);

            _context.Add(startUp);
            await _context.SaveChangesAsync();

            transaction.Commit();

            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var user = _context.Users.Find(id);

            if (user is null)
                return NotFound();

            var userViewModel = new EditStartUpFormViewModel
            {
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return PartialView("_Form", userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditStartUpFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = _context.StartUps
                .Include(ap => ap.AppUser)
                .Include(c => c.City)
                .SingleOrDefault(us => us.AppUser!.Id == model.Id);

            if (user is null)
                return NotFound();

            user.AppUser!.FirstName = model.FirstName;
            user.AppUser!.LastName = model.LastName;
            user.AppUser!.PhoneNumber = model.PhoneNumber;

            _context.SaveChanges();

            var userViewModel = new StartUpProfileViewModel
            {
                Address = user.Address!,
                City = user.City!.Name,
                CompanyName = user.StartUpName,
                Email = user.AppUser.Email!,
                EstablishDate = user.FoundingDate,
                LastName = user.AppUser.LastName,
                FirstName = user.AppUser.FirstName,
                PhoneNumber = user.AppUser.PhoneNumber,
                Id = user.Id,
                Urls = user.Url,
                IsDeleted = user.IsDeleted,
            };

            return View("Profile", userViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Block(int id)
        {
            var startUp = _context.StartUps.Find(id);

            if (startUp is null)
                return NotFound();

            startUp.IsDeleted = !startUp.IsDeleted;
            startUp.LastUpdatedOn = DateTime.Now;
            startUp.LastUpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            _context.SaveChanges();

            return Ok();
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
