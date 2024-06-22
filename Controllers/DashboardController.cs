using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Core.Consts;
using Speedy.Services.delivery;
using Speedy.Services.User;
using System.Data;

namespace Speedy.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class DashboardController(ApplicationDbContext context, IMapper mapper) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;


        public IActionResult Index()
        {
            var viewModel = new AdminDashboardViewModel
            {
                Deliveries = _context.Deliveries.Include(d => d.AppUser).ToList(),
                StartUps = _context.StartUps.Include(s => s.AppUser).ToList(),
                Individuals = _context.Individuals.Include(i => i.AppUser).ToList()
            };

            viewModel.TotalDeliveries = viewModel.Deliveries.Count;
            viewModel.TotalStartUps = viewModel.StartUps.Count;
            viewModel.TotalIndividuals = viewModel.Individuals.Count;

            return View(viewModel);
        }

    }
}
