using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using Speedy.Core.Models;
using Speedy.Services.User;
using System.Data;

namespace Speedy.Controllers
{
    public class DeliveriesController(ApplicationDbContext context, IMapper mapper, IUserService userService, IAttachmentService attachmentService, IDeliveryService deliveryService) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
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

            return View("Deliveries");

        }
        public async Task<IActionResult> Dashboard()
        {
            return View("Dashboard");
        }
        public async Task<IActionResult> Profile(/*string id*/)
        {
            //var delivery = await _deliveryService.GetDeliveryAsync(deliveryId: id);
                
            //if(delivery is null)
            //    return NotFound();

            //var deliveriesView = new DeliveryViewModel
            //{
            //    Id = delivery.Id,
            //    NID = delivery.AppUser!.NID,
            //    CreatedOn = delivery.CreatedOn,
            //    Email = delivery.AppUser.Email,
            //    IsDeleted = delivery.IsDeleted,
            //    FirstName = delivery.AppUser.FirstName,
            //    LastName = delivery.AppUser.LastName,
            //};

            return View("Profile"/*, deliveriesView*/);
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
        public IActionResult EditProfile()
        {            
            //var property = await _deliveryService.GetDeliveryAsync(id);

            //if (property is null)
            //    return NotFound();            

            return View("SettingsForm"/*, property*/);
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

