using EA_Store.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EA_Store.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories = _context.Category
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name})
                .OrderBy(x => x.Text)
                .ToList();
            var devices = _context.Device
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                .OrderBy(x => x.Text)
                .ToList();
            CreateGameFormViewModel model = new CreateGameFormViewModel()
            {
                Categories = categories,
                Devices = devices
            };
            return View(model);
        }

        //[HttpPost]
        //public IActionResult Create(CreateGameFormViewModel model)
        //{
        //    return View();
        //}
    }
}
