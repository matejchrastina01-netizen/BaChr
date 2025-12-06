using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UTB.BaChr.Mapy.Application.Abstraction;
using UTB.BaChr.Mapy.Domain.Entities;
using UTB.BaChr.Mapy.Models;

namespace UTB.BaChr.Mapy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILocationService _locationService; // 1. Definice pole pro službu

        // 2. Injektování služby pøes konstruktor
        public HomeController(ILogger<HomeController> logger, ILocationService locationService)
        {
            _logger = logger;
            _locationService = locationService;
        }

        public IActionResult Index()
        {
            // 3. Použití služby pro získání dat
            IList<Location> locations = _locationService.Select();

            // 4. Odeslání dat do View
            return View(locations);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}