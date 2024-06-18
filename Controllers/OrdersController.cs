using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Speedy.Core.Enums;
using static Speedy.Core.Enums.Variables;

namespace Speedy.Controllers
{
	public class OrdersController(ApplicationDbContext context, IMapper mapper, UserManager<AppUser> userManager) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<AppUser> _userManager = userManager;
        public IActionResult Index()
		{
            if (User.IsInRole(AppRoles.StartUp))
                return View("OrderStartup");

			if (User.IsInRole(AppRoles.Individual))
				return View("OrderIndividual");

            return View();
		}

        [HttpGet]
        public IActionResult Filter(CitiesHomeViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deliveryCitiesFliter = _context.Deliveries.Include(au => au.AppUser)
                .Where(n => n.CityId == model.CityId && !n.IsDeleted).ToList();

            if (deliveryCitiesFliter is null)
                return NotFound();

            var orderViewModel = new OrderDeliveryViewModel { Deliveries = deliveryCitiesFliter};
            
            return View("DeliveryPreview", orderViewModel);
        }

        [HttpGet]
        public IActionResult InitiateCreate(int id)
        {
            //ModelState.AddModelError(string.Empty, "Sorry, There are no appointments available right now");
            var appointmentsViewModel = new OrderFormViewModel{DeliveryId = id};

            return PartialView("_ReservationForm", appointmentsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrderFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);

            using var transaction = _context.Database.BeginTransaction();

            var order = new Order
            {
                Notes = model.Notes,
                AppUserId = model.UserId,
                CreatedById = model.UserId,
                Description = model.Description,
                IsSensitive = model.IsSensitive,                
                RecieveDate = model.RecieveDate,
                RecieverName = model.RecieverName,
                RecieverAddress = model.RecieverAddress,
                RecieverPhoneNumber = model.RecieverPhoneNumber,
                ShippingDate = model.ShippingDate,
                PaymentMethod = new PaymentMethod
                {
                    Title = "Cash",
                    HolderName = model.RecieverName,                    
                },
                
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
            transaction.Commit();

            var user = _context.Individuals
              .Include(c => c.City)
              .Include(ap => ap.AppUser)
              .SingleOrDefault(st => st.AppUserId == model.UserId);

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

            return View("~/Views/Individuals/Profile.cshtml", userView);
        }
    }
}
