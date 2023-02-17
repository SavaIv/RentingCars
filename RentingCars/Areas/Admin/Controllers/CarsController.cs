using Microsoft.AspNetCore.Mvc;

namespace RentingCars.Areas.Admin.Controllers
{
    public class CarsController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
