using Microsoft.AspNetCore.Mvc;

namespace Speedy.Controllers
{
	public class OrdersController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
