using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Core.Consts;
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
            return View("DeliveryForm", InitialDeliveryFormAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(DeliveryFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);            

            var attachResult = await _attachmentService.UploadAttachmentAsync(
                attachedFile: model.Attachments,                
                entityName: "Delivery Agents",
                userName: model.AppUserId);          

            if (!attachResult.isUploaded)
                return BadRequest(attachResult.errorMessage);

            var userForm = new UserFormViewModel
            {
                Password = model.Password,
                Email = model.Email,
                ConfirmPassword = model.ConfirmPassword,
                CreatedOn = model.CreatedOn,                
                IsActive = false,                
                SelectedRoles = AppRoles.Delivery,
                NID = model.NID
            };

            var result = await _userService.SubmitUser(userForm);

            if (!result.IsSuccess)
                return View(model);

            var delivery = new Delivery
            {
                AppUserId = result.UserId!,
                HasWhatsApp = model.HasWhatsApp,
                IsActive = false,
                Address = model.Address,
                CreatedOn = DateTime.Now,
                MobileNumber = model.MobileNumber,                                 
            };

            _context.Add(delivery);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
        private async Task<DeliveryFormViewModel> InitialDeliveryFormAsync(DeliveryFormViewModel? model = null)
        {
            DeliveryFormViewModel deliveryFormView = model ?? new DeliveryFormViewModel();

            var methodsTask = _context.ShippingMethods.Where(c => !c.IsActive).OrderBy(c => c.Name).ToListAsync();
            var citiesTask = _context.Cities.Where(c => !c.IsActive).OrderBy(c => c.Name).ToListAsync();
            var governoratesTask = _context.Governorates.Where(c => !c.IsActive).OrderBy(c => c.Name).ToListAsync();

            await Task.WhenAll(methodsTask, citiesTask, governoratesTask);

            deliveryFormView.ShippingMethods = _mapper.Map<IEnumerable<SelectListItem>>(methodsTask);
            deliveryFormView.Cities = _mapper.Map<IEnumerable<SelectListItem>>(citiesTask);
            deliveryFormView.Governorates = _mapper.Map<IEnumerable<SelectListItem>>(governoratesTask);

            return deliveryFormView;
        }        
    }
}

