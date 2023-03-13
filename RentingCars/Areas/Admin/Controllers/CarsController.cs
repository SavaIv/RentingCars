using Microsoft.AspNetCore.Mvc;
using RentingCars.Services.Cars;

namespace RentingCars.Areas.Admin.Controllers
{
    public class CarsController : AdminController
    {
        private readonly ICarService cars;

        public CarsController(ICarService _cars)
        {
            cars = _cars;
        }

        public IActionResult All()
        {
            return View(cars.All(publicOnly: false).Cars);
        }

        public IActionResult ChangeVisibility(int id)
        {
            cars.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
