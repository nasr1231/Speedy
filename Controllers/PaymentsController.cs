namespace Speedy.Controllers
{
    public class PaymentsController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        public IActionResult Index()
        {
            return View();
        }
    }
}
