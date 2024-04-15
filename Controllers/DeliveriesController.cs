using Microsoft.AspNetCore.Mvc;
using Speedy.Data;

namespace Speedy.Controllers
{
    public class DeliveriesController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        public IActionResult Index()
        {
            return View();
        }
    }
}
