﻿using Microsoft.AspNetCore.Mvc;
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

            return RedirectToAction("Index", "Home");
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
