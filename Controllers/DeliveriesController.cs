using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Core.Models;
using Speedy.Services.delivery;
using Speedy.Services.User;
using System.Data;

namespace Speedy.Controllers
{
    public class DeliveriesController(ApplicationDbContext context, IMapper mapper, UserManager<AppUser> userManager, IUserService userService, IAttachmentService attachmentService, IDeliveryService deliveryService) : Controller
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
        public IActionResult Dashboard(string id)
        {
            var delivery = _context.Deliveries
                .Include(c => c.City)
                .Include(d => d.AppUser)
                .SingleOrDefault(d => d.AppUserId == User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var orders = _context.Orders
     .Include(o => o.AppUsers)
     .Where(x => x.ShippingMethodId == delivery.ShippingMethodId && x.DeliveryId == null)
     .ToList();

            var orderViews = new List<DeliveryDashViewModel>();

            foreach (var order in orders)
            {
                var orderView = new DeliveryDashViewModel
                {
                    Description = order.Description,
                    IsSensitive = order.IsSensitive,
                    Notes = order.Notes,
                    ShippingDate = order.ShippingDate,
                    RecieverName = order.RecieverName,
                    SenderName = order.SenderName,
                    RecieverPhoneNumber = order.RecieverPhoneNumber,
                    TrackingNumber = order.TrackingNumber,
                    SenderPhoneNumber = order.SenderPhoneNumber,
                    SenderAddress = order.SenderAddress,
                    OrderAttachment = order.OrderAttachment,
                    RecieveDate = order.RecieveDate,
                    RecieverAddress = order.RecieverAddress,
                    OrderId = order.OrderId,
                };
                orderViews.Add(orderView);
            };

            return View("Dashboard", orderViews);
        }

        [HttpGet]
        public IActionResult Create()
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
                Address = model.Address,
                BirthDate = model.BirthDate,
                Gender = model.Gender
            };

            var result = await _userService.SubmitUser(userForm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                return View("DeliveryForm", InitialDeliveryForm(model));
            }

            var delivery = new Delivery
            {
                AppUserId = result.AppUser!.Id,
                HasWhatsApp = model.HasWhatsApp,
                Address = model.Address,
                CityId = model.SelectedCityId,
                ShippingMethodId = model.SelectedShippingMethod,
                IsDeleted = true                
            };

            #region Services
            var imageAttachment = await _attachmentService.UploadImageAsync(
              attachedFile: model.UserImage,
              entityName: "Delivery Agents",
             userName: delivery.AppUserId);

            if (!imageAttachment.isUploaded)
                return BadRequest(imageAttachment.errorMessage);

           result.AppUser.ProfilePictureIUrl = imageAttachment.AttachmentUrl!;

            await _userManager.UpdateAsync(result.AppUser);            

            var NationalId = await _attachmentService.UploadImageAsync(
              attachedFile: model.NationalId,
              entityName: "Delivery Agents",
             userName: delivery.AppUserId);

            if (!NationalId.isUploaded)
                return BadRequest(NationalId.errorMessage);

            delivery.NationalId= NationalId.AttachmentUrl!;

            var Criminal = await _attachmentService.UploadImageAsync(
              attachedFile: model.CriminalStatus,
              entityName: "Delivery Agents",
             userName: delivery.AppUserId);

            if (!Criminal.isUploaded)
                return BadRequest(Criminal.errorMessage);

            delivery.CriminalStatus = Criminal.AttachmentUrl!;
            #endregion

            _context.Add(delivery);
            await _context.SaveChangesAsync();

            transaction.Commit();

            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Block(int id)
        {
            var delivery = _context.Deliveries.Find(id);

            if (delivery is null)
                return NotFound();

            delivery.IsDeleted = !delivery.IsDeleted;
            delivery.LastUpdatedOn = DateTime.Now;
            delivery.LastUpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            _context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> EditProfile(string id)
        {
            var delivery = await _deliveryService.GetDeliveryAsync(deliveryId: id);

            if (delivery is null)
                return NotFound(delivery);

            var deliveryData = new DeliveryProfileFormViewModel
            {
                Id = delivery.AppUser!.Id,
                MobileNumber = delivery.AppUser!.PhoneNumber,
            };

            return View("_SettingsForm", InitiateServiceArea(deliveryData));
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(DeliveryProfileFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("SettingsForm", InitiateServiceArea(model));

            var transaction = _context.Database.BeginTransaction();

            var delivery = await _deliveryService.GetDeliverySettingsById(deliveryId: model.Id);

            if (delivery is null)
                return NotFound();

            delivery = _mapper.Map(model, delivery);
            delivery.LastUpdatedOn = DateTime.Now;

            foreach (var area in delivery.ServiceAreas)
            {
                delivery.ServiceAreas.Add(new DeliveryServiceArea { ServiceAreaId = area.ServiceAreaId });
            };

            _context.Update(delivery);
            _context.SaveChanges();

            transaction.Commit();

            return View("Profile", new { id = delivery.AppUser.Id });
        }

        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> ResetPassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return BadRequest();

            var resetPassForm = new ResetPasswordFormViewModel { Id = id };
            return PartialView("_ResetPasswordForm", resetPassForm);
        }

        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> ResetPassword(ResetPasswordFormViewModel resetForm)
        {
            if (!ModelState.IsValid)
                return BadRequest(resetForm);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == resetForm.Id);
            if (user is null)
                return BadRequest();

            var checkOld = await _userManager.CheckPasswordAsync(user, resetForm.OldPassword);
            if (!checkOld)
                return Unauthorized("Password is incorrect");

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var removeResult = await _userManager.RemovePasswordAsync(user);
                    if (!removeResult.Succeeded)
                        return BadRequest(string.Join(',', removeResult.Errors.Select(e => e.Description)));

                    var addResult = await _userManager.AddPasswordAsync(user, resetForm.Password);
                    if (!addResult.Succeeded)
                        return BadRequest(string.Join(',', addResult.Errors.Select(e => e.Description)));

                    user.LastUpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                    user.LastUpdatedOn = DateTime.Now;

                    var updateResult = await _userManager.UpdateAsync(user);
                    if (!updateResult.Succeeded)
                        return BadRequest(string.Join(',', updateResult.Errors.Select(e => e.Description)));

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

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

        public IActionResult IsUnique(DeliveryFormViewModel model)
        {

            var isExists = _context.Users.Any(c => c.Email == model.Email);

            return Json(!isExists);
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

        private DeliveryProfileFormViewModel InitiateServiceArea(DeliveryProfileFormViewModel? model = null)
        {
            DeliveryProfileFormViewModel deliveryFormView = model ?? new DeliveryProfileFormViewModel();

            var areas = _context.ServiceAreas.Where(c => !c.IsDeleted).OrderBy(c => c.Name).ToList();

            deliveryFormView.ServiceArea = _mapper.Map<IEnumerable<SelectListItem>>(areas);

            return deliveryFormView;
        }
    }
}

