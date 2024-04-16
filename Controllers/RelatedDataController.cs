using Microsoft.AspNetCore.Mvc;
using Speedy.Data;

namespace Speedy.Controllers
{
    public class RelatedDataController(ApplicationDbContext context, IMapper mapper) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
