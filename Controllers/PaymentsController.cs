using Microsoft.AspNetCore.Mvc.Rendering;

namespace Speedy.Controllers
{
    public class PaymentsController(ApplicationDbContext context, IMapper mapper) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> Create(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user is null)
                return NotFound();
            var paymentList = new PaymentFormViewModel
            {
                Id = userId
            };

            return PartialView("_Form", paymentList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(PaymentFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transaction = _context.Database.BeginTransaction();

            var method = new PaymentMethod
            {
                AppUserId = model.Id,
                handle = model.handle,
                HolderName = model.HolderName,
                PhoneNumber = model.PhoneNumber,
                Title = model.Name,
            };

            _context.PaymentMethods.Add(method);
            _context.SaveChanges();

            transaction.Commit();

            return Ok();
        }
    }
}
