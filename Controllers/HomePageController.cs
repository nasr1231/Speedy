using Microsoft.AspNetCore.Mvc;

namespace Speedy.Controllers
{
    public class HomePageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
