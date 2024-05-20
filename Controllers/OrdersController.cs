using Microsoft.AspNetCore.Mvc;

namespace Speedy.Controllers
{
	public class OrdersController : Controller
	{
		public IActionResult Index()
		{
            if (User.IsInRole(AppRoles.StartUp))
                return View("OrderStartup");

            return View();
		}
	}
}
