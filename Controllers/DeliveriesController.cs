using Microsoft.AspNetCore.Mvc;
using Speedy.Core.Consts;
using Speedy.Data;
using System.Data;

namespace Speedy.Controllers
{
    public class DeliveriesController(ApplicationDbContext context, IMapper mapper) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public IActionResult Index()
        {

            if (User.IsInRole(AppRoles.Admin))
                return View("Index");            
            
            return View("Deliveries");

        }

        public IActionResult Create()
        {

            return View("Form");
        }
    }
}
