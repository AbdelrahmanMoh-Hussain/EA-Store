using EA_Store.Services;
using EA_Store.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EA_Store.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IDevicesService _devicesService;
        private readonly IGamesService _gamesService;
        public GamesController(ICategoriesService categoriesServices, IDevicesService devicesService, IGamesService gamesServices)
        {
            _categoriesService = categoriesServices;
            _devicesService = devicesService;
            _gamesService = gamesServices;
        }

        public IActionResult Index()
        {
            return View(_gamesService.GetAllGames());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories = _categoriesService.GetCategories();
            var devices = _devicesService.GetDevices();
            CreateGameFormViewModel model = new CreateGameFormViewModel()
            {
                Categories = categories,
                Devices = devices
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetCategories();
                model.Devices = _devicesService.GetDevices();
               return View(model);
            }
            
            await _gamesService.Create(model);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var game = _gamesService.GetGameById(id);
            return game is null ?  NotFound() : View(game);
        }

        public IActionResult Edit(int id)
        {
            var game = _gamesService.GetGameById(id);
            if (game is null)
                return NotFound();

            EditGameFormViewModel viewModel = new EditGameFormViewModel()
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _categoriesService.GetCategories(),
                Devices = _devicesService.GetDevices(),
                CurrentCover = game.Cover,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetCategories();
                model.Devices = _devicesService.GetDevices();
                return View(model);
            }

            var game = await _gamesService.Update(model);

            return game is null ? NotFound() : RedirectToAction(nameof(Index));
        }
    }
}
