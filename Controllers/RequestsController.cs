using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Speedy.Core.Models;
using Speedy.Core.Models.RelatedData;
using Speedy.Services.User;
using System.Security.Cryptography;

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

            view.DeliveryId = id;

            return View("Index", view);
        }

        public IActionResult Accept(int id)
        {
            var delivery = _context.Deliveries.SingleOrDefault(x => x.Id == id);
            if (delivery is null)
                return NotFound();

            delivery!.IsDeleted = false;

            _context.Update(delivery);
            _context.SaveChanges();

            var requests = _context.Deliveries
                .Include(ap => ap.AppUser)
                .Where(de => de.IsDeleted).ToList();

            var servicesView = _mapper.Map<IEnumerable<RequestViewModel>>(requests);

            return RedirectToAction("GetRequests", "Requests", servicesView);
        }

        public IActionResult Reject(int id)
        {
            var delivery = _context.Deliveries.SingleOrDefault(x => x.Id == id);
            if (delivery is null)
                return NotFound();

            var user = _context.Users.SingleOrDefault(x => x.Id == delivery.AppUserId);

            user!.IsActive = false;

            _context.Update(user);
            _context.SaveChanges();

            var requests = _context.Deliveries
                .Include(ap => ap.AppUser)
                .Where(de => de.IsDeleted).ToList();

            var servicesView = _mapper.Map<IEnumerable<RequestViewModel>>(requests);

            return RedirectToAction("GetRequests", "Requests", servicesView);
        }
    }
}
