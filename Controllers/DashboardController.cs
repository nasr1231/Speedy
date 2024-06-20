using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Speedy.Core.Consts;
using System.Data;

namespace Speedy.Controllers
{
	[Authorize(Roles = AppRoles.Admin)]
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
