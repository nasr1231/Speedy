using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Speedy.Core.Models.RelatedData;
using Speedy.Services.User;

namespace Speedy.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class CitiesController(ApplicationDbContext context, IMapper mapper) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;        
        public IActionResult Index()
        {
            var cities = _context.Cities
                 .Include(c => c.Governorate)
                 .AsNoTracking()
                 .ToList();

            if (cities == null)
                return NotFound();

            var servicesView = _mapper.Map<IEnumerable<CityViewModel>>(cities);
            return View(servicesView);
        }

        
        public IActionResult Create()
        {
            return PartialView("_Form", InitialCityForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CityFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Form", InitialCityForm(model));

            //City city = new()
            //{
            //    CreatedOn = model.CreatedOn,
            //    GovernorateId = model.GovernorateId,
            //    IsDeleted = false,
            //    Name = model.Name,
            //    CreatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value,                
            //};            

            var cityArea = _mapper.Map<City>(model);
            cityArea.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            _context.Cities.Add(cityArea);
            _context.SaveChanges();

            var cityView = _mapper.Map<CityViewModel>(cityArea);

            return PartialView("_NewRow", cityView);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var selectedCity = _context.Cities.SingleOrDefault(b => b.Id == id);

            if (selectedCity is null)
                return NotFound();

            var propertyFormView = _mapper.Map<CityFormViewModel>(selectedCity);
            propertyFormView = InitialCityForm(propertyFormView);

            return PartialView("_Form", propertyFormView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CityFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(("_Form", InitialCityForm(model)));

            var city = _context.Cities.Find(model.Id);

            city = _mapper.Map(model, city);

            city!.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            city.LastUpdatedOn = DateTime.Now.ToUniversalTime();

            _context.Cities.Update(city!);
            _context.SaveChanges();

            var cityView = _mapper.Map<CityViewModel>(city);

            return PartialView("_NewRow", cityView);
        }

        public IActionResult IsExist(CityFormViewModel model)
        {
            var Geographical = _context.Cities.SingleOrDefault(c => c.Name == model.Name);

            var IsAllowed = Geographical is null || Geographical.Id == model.Id;

            return Json(IsAllowed);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleStatus(int Id)
        {
            var type = _context.Cities.Find(Id);

            if (type is null)
                return NotFound();
            
            type.LastUpdatedOn = DateTime.Now.ToUniversalTime();
            type.LastUpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            _context.Cities.Update(type);
            _context.SaveChanges();

            var typeView = _mapper.Map<CityViewModel>(type);

            return PartialView("_NewRow", typeView);
        }

        private CityFormViewModel? InitialCityForm(CityFormViewModel? model = null)
        {
            CityFormViewModel cityFormView = model is null ? new CityFormViewModel() : model;
            var Governorates = _context.Governorates.Where(c => !c.IsDeleted).OrderBy(c => c.Name).ToList();

            cityFormView.Governorate = _mapper.Map<IEnumerable<SelectListItem>>(Governorates);

            return cityFormView;
        }
    }
}
