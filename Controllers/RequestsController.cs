using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Speedy.Core.Models.RelatedData;
using Speedy.Services.User;

namespace Speedy.Controllers
{
    public class RequestsController(ApplicationDbContext context, IMapper mapper, UserManager<AppUser> userManager) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<AppUser> _userManager = userManager;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetRequests()
        {
            var requests = _context.Deliveries
                .Include(ap => ap.AppUser)
                .Where(de => de.IsDeleted).ToList();
            
            var servicesView = _mapper.Map<IEnumerable<RequestViewModel>>(requests);


            return View("DeliveryRequestPanel", servicesView);
        }

        public IActionResult Details(int id)
        {
            var delivery = _context.Deliveries.Include(ap => ap.AppUser).SingleOrDefault(x => x.Id == id);

            if (delivery is null)
                return NotFound();

            var view = _mapper.Map<RequestDetailsViewModel>(delivery);

            return View("Index", view);
        }
    }
}
