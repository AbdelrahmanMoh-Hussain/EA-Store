using EA_Store.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EA_Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGamesService _gamesService;

        public HomeController(IGamesService gamesService)
        {
            _gamesService = gamesService;
        }

        public IActionResult Index()
        {
            return View(_gamesService.GetAllGames());
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
