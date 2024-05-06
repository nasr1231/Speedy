using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Speedy.Core.Models.RelatedData;

namespace Speedy.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class ShippingMethodsController(ApplicationDbContext context, IMapper mapper) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public IActionResult Index()
        {
            var shippingMethods = _context.ShippingMethods
                 .AsNoTracking()
                 .ToList();

            if (shippingMethods == null)
                return NotFound();

            var servicesView = _mapper.Map<IEnumerable<ShippingMethodViewModel>>(shippingMethods);
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
        public IActionResult Create(ShippingMethodFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var shipping = _mapper.Map<ShippingMethod>(model);
            shipping.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            _context.ShippingMethods.Add(shipping);
            _context.SaveChanges();

            var shippingView = _mapper.Map<ShippingMethodViewModel>(shipping);

            return PartialView("_NewRow", shippingView);
        }


        [HttpGet]
        [AjaxOnly]
        public IActionResult Edit(int Id)
        {
            var content = _context.ShippingMethods.Find(Id);

            if (content is null)
                return NotFound();

            var typeFormView = _mapper.Map<ShippingMethodFormViewModel>(content);

            return PartialView("_Form", typeFormView);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ShippingMethodFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var shipping = _context.ShippingMethods.Find(model.Id);

            if (shipping is null)
                return NotFound();

            shipping = _mapper.Map(model, shipping);
            shipping.LastUpdatedOn = DateTime.Now;
            shipping.LastUpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;


            _context.ShippingMethods.Update(shipping);
            _context.SaveChanges();

            var shippinglView = _mapper.Map<ShippingMethodViewModel>(shipping);


            return PartialView("_NewRow", shippinglView);
        }


        public IActionResult IsExist(ShippingMethodFormViewModel model)
        {
            var shipping = _context.ShippingMethods.SingleOrDefault(c => c.Name == model.Name);

            var IsAllowed = shipping is null || shipping.Id == model.Id;

            return Json(IsAllowed);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleStatus(int Id)
        {
            var type = _context.ShippingMethods.Find(Id);

            if (type is null)
                return NotFound();

            type.IsDeleted = !type.IsDeleted;
            type.LastUpdatedOn = DateTime.Now.ToUniversalTime();
            type.LastUpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            _context.ShippingMethods.Update(type);
            _context.SaveChanges();

            var typeView = _mapper.Map<ShippingMethodFormViewModel>(type);

            return PartialView("_NewRow", typeView);
        }
    }
}
