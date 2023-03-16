using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using RentingCars.Infrastructure;
using RentingCars.Models.Cars;
using RentingCars.Services.Cars;
using RentingCars.Services.Dealers;
using static RentingCars.Infrastructure.ClaimsPrincipalExtensions;

using static RentingCars.WebConstants;

namespace RentingCars.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService cars;
        private readonly IDealerService dealers;
        private readonly IMapper mapper;

        public CarsController(
            ICarService _cars, 
            IDealerService _dealers, 
            IMapper _mapper)
        {
            cars = _cars;
            dealers = _dealers;
            mapper = _mapper;
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myCars = cars.ByUser(User.Id());

            return View(myCars);
        }

        public IActionResult Details(int id, string information)
        {
            var car = cars.Details(id);

            if(information != car.GetInformation())
            {
                return BadRequest();
            }

            return View(car);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!dealers.IsDealer(User.Id()))
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            return View(new CarFormModel
            {
                Categories = cars.AllCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(CarFormModel car)
        {
            var dealerId = dealers.IdbyUser(User.Id());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!cars.CategoryExists(car.CategoryId))
            {
                ModelState.AddModelError(nameof(car.CategoryId), "Category dose not exist!");
            }

            if (!ModelState.IsValid)
            {
                car.Categories = cars.AllCategories();

                return View(car);
            }

            var carId = cars.Create(car.Brand,
                car.Model,
                car.Description,
                car.ImageUrl,
                car.CategoryId,
                car.Year,
                dealerId);

            TempData[GlobalMessageKey] = "You car was saved successfuly and waiting for approval!";

            return RedirectToAction(nameof(Details), new { id = carId, information = car.GetInformation()});
        }

        public IActionResult All([FromQuery] AllCarsQueryModel query)
        {
            var queryResult = cars.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllCarsQueryModel.CarsPerPage);

            var carBrands = cars.AllBrands();

            query.TotalCars = queryResult.TotalCars;
            query.Brands = carBrands;
            query.Cars = queryResult.Cars;

            return View(query);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = User.Id();

            if (!dealers.IsDealer(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            var car = cars.Details(id);

            if (car.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var carForm = mapper.Map<CarFormModel>(car);

            carForm.Categories = cars.AllCategories();

            return View(carForm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, CarFormModel car)
        {
            var dealerId = dealers.IdbyUser(User.Id());

            if (dealerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!cars.CategoryExists(car.CategoryId))
            {
                ModelState.AddModelError(nameof(car.CategoryId), "Category dose not exist!");
            }

            if (!ModelState.IsValid)
            {
                car.Categories = cars.AllCategories();

                return View(car);
            }

            if (!cars.IsByDealer(id, dealerId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            var edited = cars.Edit(
                id,
                car.Brand,
                car.Model,
                car.Description,
                car.ImageUrl,
                car.CategoryId,
                car.Year,
                User.IsAdmin());

            if (!edited)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = $"The car was edited {(User.IsAdmin() ? string.Empty : " and waiting for approval")}! ";

            return RedirectToAction(nameof(Details), new { id, information = car.GetInformation() });
        }
    }
}
