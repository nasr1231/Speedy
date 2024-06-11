using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var requests = _context.Deliveries.Where(de => de.IsDeleted).ToList();

            var requestsViewModel = new RequestViewModel { Deliveries = requests };

            return View("DeliveryRequestPanel", requestsViewModel);
        }
    }
}
