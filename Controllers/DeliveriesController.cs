using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Speedy.Core.Models;
using Speedy.Services.User;
using System.Data;
using System.Diagnostics;

namespace Speedy.Controllers
{
    public class DeliveriesController(ApplicationDbContext context, IMapper mapper,UserManager<AppUser> userManager, IUserService userService, IAttachmentService attachmentService, IDeliveryService deliveryService) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IUserService _userService = userService;
        private readonly IAttachmentService _attachmentService = attachmentService;
        private readonly IDeliveryService _deliveryService = deliveryService;


        public async Task<IActionResult> Index()
        {
            var deliveries = await _deliveryService.GetAllDeliveriesAsync();

            if (deliveries == null)
                return NotFound();

            var deliveriesView = deliveries.Select(d => new DeliveryViewModel
            {
                Id = d.Id,
                NID = d.AppUser!.NID,
                CreatedOn = d.CreatedOn,
                Email = d.AppUser.Email,
                IsDeleted = d.IsDeleted,
                FirstName = d.AppUser.FirstName,
                LastName = d.AppUser.LastName,
            });

            if (User.IsInRole(AppRoles.Admin))
                return View("Index", deliveriesView);

            if (User.IsInRole(AppRoles.Delivery))
                return View("Dashboard", deliveriesView);

            if (User.IsInRole(AppRoles.StartUp))
                return View("StartUps", deliveriesView);

            if (User.IsInRole(AppRoles.Individual))
                return View("Individuals", deliveriesView);

            return NotFound();
        }
        public async Task<IActionResult> Dashboard(string id)
        {
            var delivery = await _deliveryService.GetDeliveryAsync(deliveryId: id);

            if (delivery is null)
                return NotFound();

            var deliveriesView = _mapper.Map<DeliveryViewModel>(delivery);

            return View("Dashboard", deliveriesView);
        }
        public async Task<IActionResult> Profile(string id)
        {
            var delivery = await _deliveryService.GetDeliveryAsync(deliveryId: id);

            if (delivery is null)
                return NotFound();
         
            var deliveriesView = _mapper.Map<DeliveryViewModel>(delivery);            

            return View("Profile", deliveriesView);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View("DeliveryForm", InitialDeliveryForm());
        }

        [HttpPost]
        public async Task<IActionResult> Create(DeliveryFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("DeliveryForm", InitialDeliveryForm(model));

            using var transaction = _context.Database.BeginTransaction();

            var userForm = new UserFormViewModel
            {
                Password = model.Password,
                Email = model.Email,
                ConfirmPassword = model.ConfirmPassword,
                SelectedRoles = AppRoles.Delivery,
                NID = model.NID,
                PhoneNumber = model.MobileNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var result = await _userService.SubmitUser(userForm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                return View("DeliveryForm", InitialDeliveryForm(model));
            }

            var delivery = new Delivery
            {
                AppUserId = result.UserId!,
                HasWhatsApp = model.HasWhatsApp,
                Address = model.Address,
                CityId = model.SelectedCityId,
                ShippingMethodId = model.SelectedShippingMethod,
                IsDeleted = true
            };

            var attachResult = await _attachmentService.UploadAttachmentAsync(
                attachedFile: model.Attachments,                
                entityName: "Delivery Agents",
                userName: delivery.AppUserId);

            if (!attachResult.isUploaded)
                return BadRequest(attachResult.errorMessage);

            _context.Add(delivery);
            await _context.SaveChangesAsync();

            transaction.Commit();

            return RedirectToAction("Index", "Home");
        }

        
        //[Authorize(Roles = AppRoles.Delivery)]
        public async Task<IActionResult> EditProfile(string id)
        {
            var delivery = await _deliveryService.GetDeliveryAsync(deliveryId: id);

            if (delivery is null)
                return NotFound();

            var deliveryData = _mapper.Map<DeliveryProfileFormViewModel>(delivery);

            return View("SettingsForm", deliveryData);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(DeliveryProfileFormViewModel model)
        {
            var agent = _context.Deliveries.SingleOrDefaultAsync(a => a.Id == model.Id);

            return View("Profile");
        }

        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> ResetPassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return BadRequest();           

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetPassForm = new ResetPasswordFormViewModel {
                Code = code,
                Id = id
            };            
            return PartialView("_ResetPasswordForm", resetPassForm);
        }        

        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> ResetPassword(ResetPasswordFormViewModel resetForm)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var transaction = _context.Database.BeginTransaction();
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == resetForm.Id);

            if (user is null)
                return BadRequest();

            var checkOld = await _userManager.CheckPasswordAsync(user, resetForm.OldPassword);

            if (!checkOld)
                return Unauthorized("Password is incorrect");

            var result = await _userManager.ResetPasswordAsync(user, resetForm.Code, resetForm.Password);

            if (!result.Succeeded)
                return BadRequest(string.Join(',', result.Errors.Select(e => e.Description)));

            user.LastUpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            user.LastUpdatedOn = DateTime.Now;

            var UserResult = await _userManager.UpdateAsync(user);

            if (!UserResult.Succeeded)
                return BadRequest(string.Join(',', result.Errors.Select(e => e.Description)));
            transaction.Commit();

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleStatus(int id)
        {
            IQueryable<Delivery> userQueryable = _context.Deliveries;

            var delivery = userQueryable.SingleOrDefault(b => b.Id == id);

            if (delivery is null)
                return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            delivery.IsDeleted = !delivery.IsDeleted;
            delivery.LastUpdatedOn = DateTime.Now;
            delivery.LastUpdatedById = userId;

            _context.Deliveries.Update(delivery);
            _context.SaveChanges();

            return Ok();
        }

        public IActionResult GetCities(int GovernorateId)
        {
            var cities = _context.Cities.Where(c => !c.IsDeleted && c.GovernorateId == GovernorateId).OrderBy(c => c.Name).ToList();

            var citiesSelectListItem = _mapper.Map<IEnumerable<SelectListItem>>(cities);

            return Ok(citiesSelectListItem);
        }

        private DeliveryFormViewModel InitialDeliveryForm(DeliveryFormViewModel? model = null)
        {
            DeliveryFormViewModel deliveryFormView = model ?? new DeliveryFormViewModel();

            var methodsTask = _context.ShippingMethods.Where(c => !c.IsDeleted).OrderBy(c => c.Name).ToList();
            //var citiesTask = _context.Cities.Where(c => !c.IsDeleted).OrderBy(c => c.Name).ToList();
            var governoratesTask = _context.Governorates.Where(c => !c.IsDeleted).OrderBy(c => c.Name).ToList();

            deliveryFormView.ShippingMethods = _mapper.Map<IEnumerable<SelectListItem>>(methodsTask);
            deliveryFormView.Governorates = _mapper.Map<IEnumerable<SelectListItem>>(governoratesTask);

            return deliveryFormView;
        }
    }
}

