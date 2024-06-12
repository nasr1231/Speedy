using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

		public IActionResult Agents()
		{
			if(User.IsInRole(AppRoles.Individual))
			return View("Form");

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

            var orderViewModel = new OrderFormViewModel { Deliveries = deliveryCitiesFliter};
            
            return View("DeliveryPreview", orderViewModel);
        }

        [HttpGet]
        public IActionResult InitiateCreate(int id)
        {
            //ModelState.AddModelError(string.Empty, "Sorry, There are no appointments available right now");
            var appointmentsViewModel = new OrderFormViewModel{DeliveryId = id};

            return PartialView("_ReservationForm", appointmentsViewModel);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(ReservationFormViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var reserve = model.MapToModel(User.GetUserId());

        //    _dbContext.Add(reserve);
        //    _dbContext.SaveChanges();

        //    var newAppoinment = _dbContext.Reservations.FirstOrDefault(a => a.Id == reserve.Id);

        //    if (newAppoinment is null)
        //        return NotFound();

        //    return View("PatientReservations");
        //}
    }
}
