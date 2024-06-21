using Microsoft.AspNetCore.Mvc;

namespace Speedy.Controllers
{
    public class OrderAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
