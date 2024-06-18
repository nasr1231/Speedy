using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Speedy.Core.Models;
using Speedy.Core.ViewModels;
using System.Xml.Linq;

namespace Speedy.Controllers
{
    public class FeedBacksController(ApplicationDbContext context, IMapper mapper, UserManager<AppUser> userManager) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<AppUser> _userManager = userManager;
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
                CreatedOn = DateTime.Now,
            };

            _context.Feedbacks.Add(feedbackMessage);
            _context.SaveChanges();

            transaction.Commit();

            // You can pass a simple string message
            ViewBag.SuccessMessage = "تم تسجيل رأيك بنجاح";

            return View("Index");
        }

        public IActionResult Dashboard()
        {
            var feedbacks = _context.Feedbacks.Include(A => A.User).ToList();
            var viewModels = new List<FeedbacksDashboardViewModel>();

            foreach (var feed in feedbacks)
            {
                var viewModel = new FeedbacksDashboardViewModel
                {
                    CreationDate = feed.CreatedOn,
                    Id = feed.Id,
                    UserId = feed.UserId,
                    UserName = $"{feed.User.FirstName} {feed.User.LastName}"
                };

                viewModels.Add(viewModel);
            }

            return View("Dashboard", viewModels);
        }
        public IActionResult Message(int id)
        {
            var message = _context.Feedbacks
                .Include(ap => ap.User)
                .SingleOrDefault(i => i.Id == id);

            if (message is null)
                return NotFound(message);

            var feedViewModel = new FeedbackViewModel
            {
                CreatedOn = message.CreatedOn,
                Message = message.Message,
                UserId = message.UserId,
                UserName = $"{message.User.FirstName} {message.User.LastName}"
            };

            return PartialView("_MessageForm", feedViewModel);
        }

    }
}
