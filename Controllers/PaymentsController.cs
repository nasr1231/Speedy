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

        public async Task<IActionResult> Create(int userId)
        {
            var user = await _context.Deliveries.FindAsync(userId);
            if (user is null)
                return NotFound();
            var paymentList = new PaymentFormViewModel
            {
               Id = userId
            };

            return PartialView("_Form", InitiatePaymentMethods(paymentList));
        }
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> Create(PaymentFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok();
        }

        private PaymentFormViewModel InitiatePaymentMethods(PaymentFormViewModel ? model = null)
        {
            PaymentFormViewModel paymentsViewModel = model ?? new PaymentFormViewModel();

            var payments = _context.PaymentMethods.Where(c => !c.IsDeleted).OrderBy(c => c.Title).ToList();

            paymentsViewModel.PaymentMethods = _mapper.Map<IEnumerable<SelectListItem>>(payments);

            return paymentsViewModel;
        }
    }
}
