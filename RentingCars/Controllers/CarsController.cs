using Microsoft.AspNetCore.Mvc;
using RentingCars.Data;
using RentingCars.Data.Models;
using RentingCars.Models.Cars;

namespace RentingCars.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext data;

        public CarsController(ApplicationDbContext _data)
        {
            data = _data;
        }

        public IActionResult Add()
        {
            return View(new AddCarFormModel
            {
                Categories = GetCarCategories()
            });
        }

        [HttpPost]
        public IActionResult Add(AddCarFormModel car)
        {
            if(!data.Categories.Any(c => c.Id == car.CategoryId))
            {
                ModelState.AddModelError(nameof(car.CategoryId), "Category dose not exist!");
            }

            if (!ModelState.IsValid)
            {
                car.Categories = GetCarCategories();

                return View(car);
            }

            var theCar = new Car
            {
                Brand = car.Brand,
                Model = car.Model,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                CategoryId = car.CategoryId,
                Year = car.Year
            };

            data.Cars.Add(theCar);
            data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery]AllCarsQueryModel query)
        {
            var carsQuery = data.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Brand))
            {
                carsQuery = carsQuery.Where(c => c.Brand == query.Brand);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                carsQuery = carsQuery.Where(c =>
                    c.Brand.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    c.Model.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    c.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            carsQuery = query.Sorting switch
            {
                CarSorting.DateCreated => carsQuery.OrderByDescending(c => c.Id),
                CarSorting.Year => carsQuery.OrderByDescending(c => c.Year),
                CarSorting.BrandAndModel => carsQuery.OrderBy(c => c.Brand).ThenBy(c => c.Model),
                _ => carsQuery.OrderByDescending(c => c.Id)
            };

            var cars = carsQuery                
                .Select(c => new CarListingViewModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    ImageUrl = c.ImageUrl,
                    Category = c.Category.Name
                })
                .ToList();

            var carBrands = data
                .Cars
                .Select(c => c.Brand)
                .OrderBy(br => br)
                .Distinct()
                .ToList();

            query.Brands = carBrands;
            query.Cars = cars;

            return View(query);
        }

        private IEnumerable<CarCategoryViewModel> GetCarCategories()
        {
            return data
                    .Categories
                    .Select(c => new CarCategoryViewModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToList();
        }
    }
}
