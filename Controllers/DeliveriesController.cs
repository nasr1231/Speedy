using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Services.User;

namespace Speedy.Controllers
{
    public class DeliveriesController(ApplicationDbContext context, IMapper mapper, IUserService userService, IAttachmentService attachmentService) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IUserService _userService = userService;
        private readonly IAttachmentService _attachmentService = attachmentService;

        public IActionResult Index()
        {

            if (User.IsInRole(AppRoles.Admin))
                return View("Index");

            return View("Deliveries");

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
                PhoneNumber = model.MobileNumber
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

