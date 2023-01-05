using Microsoft.AspNetCore.Mvc;

namespace RentingCars.Controllers
{
    public class DealersController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
