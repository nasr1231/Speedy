using Microsoft.AspNetCore.Mvc;

namespace Speedy.Controllers
{
    public class FeedBacksController(ApplicationDbContext context, IMapper mapper) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Complain(FeedbackViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);

            var transaction = _context.Database.BeginTransaction();

            var feedbackMessage = new Feedback
            {
                UserId = model.UserId,
                Message = model.Message,
            };

            _context.Feedbacks.Add(feedbackMessage);
            _context.SaveChanges();

            transaction.Commit();

            // You can pass a simple string message
            ViewBag.SuccessMessage = "تم تسجيل رأيك بنجاح";

            return View("Index");
        }

    }
}
