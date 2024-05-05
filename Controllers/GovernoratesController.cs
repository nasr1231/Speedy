using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Speedy.Core.Models.RelatedData;

namespace Speedy.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class GovernoratesController(ApplicationDbContext context, IMapper mapper) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public IActionResult Index()
        {
            var Governorates = _context.Governorates
                 .AsNoTracking()
                 .ToList();

            if (Governorates == null)
                return NotFound();

            var servicesView = _mapper.Map<IEnumerable<GovernorateViewModel>>(Governorates);
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
        public IActionResult Create(GovernorateFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var GovernorateArea = _mapper.Map<Governorate>(model);
            GovernorateArea.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            _context.Governorates.Add(GovernorateArea);
            _context.SaveChanges();

            var GovernorateView = _mapper.Map<GovernorateViewModel>(GovernorateArea);

            return PartialView("_NewRow", GovernorateView);
        }


        [HttpGet]
        [AjaxOnly]
        public IActionResult Edit(int Id)
        {
            var content = _context.Governorates.Find(Id);

            if (content is null)
                return NotFound();

            var typeFormView = _mapper.Map<GovernorateFormViewModel>(content);

            return PartialView("_Form", typeFormView);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GovernorateFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var geographical = _context.Governorates.Find(model.Id);

            if (geographical is null)
                return NotFound();

            geographical = _mapper.Map(model, geographical);
            geographical.LastUpdatedOn = DateTime.Now.ToUniversalTime();
            geographical.LastUpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;


            _context.Governorates.Update(geographical);
            _context.SaveChanges();

            var geographicalView = _mapper.Map<GovernorateFormViewModel>(geographical);


            return PartialView("_NewRow", geographicalView);
        }


        public IActionResult IsExist(GovernorateFormViewModel model)
        {
            var Geographical = _context.Governorates.SingleOrDefault(c => c.Name == model.Name);

            var IsAllowed = Geographical is null || Geographical.Id == model.Id;

            return Json(IsAllowed);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleStatus(int Id)
        {
            var type = _context.Governorates.Find(Id);

            if (type is null)
                return NotFound();

            type.IsActive = !type.IsActive;
            type.LastUpdatedOn = DateTime.Now.ToUniversalTime();
            type.LastUpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            _context.Governorates.Update(type);
            _context.SaveChanges();

            var typeView = _mapper.Map<GovernorateFormViewModel>(type);

            return PartialView("_NewRow", typeView);
        }
    }
}
