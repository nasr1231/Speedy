using Speedy.Core.Consts;
using Speedy.Services.User;

namespace Speedy.Controllers
{
    public class DeliveriesController(ApplicationDbContext context, IMapper mapper, IUserService userService) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IUserService _userService = userService;

        public IActionResult Index()
        {

            if (User.IsInRole(AppRoles.Admin))
                return View("Index");

            return View("Deliveries");

        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var deliveryForm = new DeliveryFormViewModel();

            return View(deliveryForm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DeliveryFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userForm = new UserFormViewModel
            {
                //Password= model.Password,
                //Email= model.Email,
                //.....
                SelectedRoles = AppRoles.Delivery
            };

            var result = await _userService.SubmitUser(userForm);

            if (!result.IsSuccess)
                return View(model);

            var delivery = new Delivery
            {
                AppUserId = result.UserId!,
                HasWhatsApp

            };

            _context.Add(delivery);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}

