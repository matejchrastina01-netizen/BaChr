using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UTB.BaChr.Mapy.Application.Abstraction;
using UTB.BaChr.Mapy.Domain.Entities;

namespace UTB.BaChr.Mapy.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // Jen pro admina
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public IActionResult Index()
        {
            IList<Location> locations = _locationService.Select();
            return View(locations);
        }

        // Zde přidej akce Create, Edit, Delete volající metody servisu
        // Nezapomeň, že v Create metodě při POSTu musíš validovat ModelState.IsValid
    }
}