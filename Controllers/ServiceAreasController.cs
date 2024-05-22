using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Speedy.Core.Models.RelatedData;
using Speedy.Services.User;

namespace Speedy.Controllers
{
    public class ServiceAreasController(ApplicationDbContext context, IMapper mapper, UserManager<AppUser> userManager) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<AppUser> _userManager = userManager;
               
        public IActionResult Index()
        {
            var areas = _context.ServiceAreas
                 .AsNoTracking()
                 .ToList();

            if (areas == null)
                return NotFound();

            var servicesView = _mapper.Map<IEnumerable<ServiceAreaViewModel>>(areas);
            return View(servicesView);
        }

        [HttpGet]
        [AjaxOnly]
        public IActionResult Create()
        {
            return PartialView("_Form");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceAreaFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var serviceArea = _mapper.Map<ServiceArea>(model);
            serviceArea.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            _context.ServiceAreas.Add(serviceArea);
            _context.SaveChanges();

            var areasView = _mapper.Map<ServiceAreaViewModel>(serviceArea);

            return PartialView("_NewRow", areasView);
        }

        [HttpGet]
        [AjaxOnly]
        public IActionResult Edit(int Id)
        {
            var content = _context.ServiceAreas.Find(Id);

            if (content is null)
                return NotFound();

            var typeFormView = _mapper.Map<ServiceAreaFormViewModel>(content);

            return PartialView("_Form", typeFormView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ServiceAreaFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var areas = _context.ServiceAreas.Find(model.Id);

            if (areas is null)
                return NotFound();

            areas = _mapper.Map(model, areas);
            areas.LastUpdatedOn = DateTime.Now;
            areas.LastUpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;


            _context.ServiceAreas.Update(areas);
            _context.SaveChanges();

            var areasView = _mapper.Map<ServiceAreaViewModel>(areas);


            return PartialView("_NewRow", areasView);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleStatus(int Id)
        {
            var transaction = _context.Database.BeginTransaction();

            var type = _context.ServiceAreas.Find(Id);

            if (type is null)
                return NotFound();

            type.IsDeleted = !type.IsDeleted;
            type.LastUpdatedOn = DateTime.Now.ToUniversalTime();
            type.LastUpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            _context.ServiceAreas.Update(type);
            _context.SaveChanges();

            transaction.Commit();

            var typeView = _mapper.Map<ServiceAreaViewModel>(type);

            return PartialView("_NewRow", typeView);
        }
    }
}
