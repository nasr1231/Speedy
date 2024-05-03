using Microsoft.AspNetCore.Mvc;
using Speedy.Core.Consts;
using Speedy.Services.User;

namespace Speedy.Controllers
{
    public class IndividualsController(ApplicationDbContext context, IMapper mapper, IUserService userService) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IUserService _userService = userService;

        public IActionResult Index()
        {
            return View();
        }        

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var individualForm = new IndividualFormViewModel();

            return View("IndividualForm");
        }

        [HttpPost]
        public async Task<IActionResult> Create(IndividualFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userForm = new UserFormViewModel
            {
                //Password= model.Password,
                //Email= model.Email,
                //.....
                SelectedRoles = AppRoles.Individual
            };

            var result = await _userService.SubmitUser(userForm);

            if (!result.IsSuccess)
                return View(model);

            var individual = new Individual
            {
                AppUserId = result.UserId!,
                

            };

            _context.Add(individual);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
