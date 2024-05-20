using Microsoft.AspNetCore.Mvc;

namespace Speedy.Controllers
{
	public class OrdersController : Controller
	{
		public IActionResult Index()
		{
            if (User.IsInRole(AppRoles.StartUp))
                return View("OrderStartup");

			if (User.IsInRole(AppRoles.Individual))
				return View("OrderIndividual");

            return View();
		}

		public IActionResult Order()
		{
			if(User.IsInRole(AppRoles.StartUp))
			return View("Form");

			return View("Form");
		}
	}
}
