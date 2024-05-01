using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Speedy.Controllers
{	
	public class DashboardController : Controller
	{
		private readonly ILogger<DashboardController> _logger;
		public DashboardController(ILogger<DashboardController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
