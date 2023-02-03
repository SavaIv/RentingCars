﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentingCars.Data;
using RentingCars.Data.Models;
using RentingCars.Models;
using RentingCars.Models.Cars;
using RentingCars.Services.Cars;
using RentingCars.Services.Dealers;
using static RentingCars.Infrastructure.ClaimsPrincipalExtensions;

namespace RentingCars.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService cars;
        private readonly IDealerService dealers;
        private readonly ApplicationDbContext data;

        public CarsController(ICarService _cars, ApplicationDbContext _data, IDealerService _dealers)
        {
            cars = _cars;
            data = _data;
            dealers = _dealers;
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myCars = cars.ByUser(User.GetId());

            return View(myCars);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!dealers.IsDealer(User.GetId()))
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            var categories = cars.AllCategories();

            return View(new CarFormModel
            {
                Categories = categories
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(CarFormModel car)
        {
            var dealerId = dealers.GetIdbyUser(User.GetId());

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

            cars.Create(car.Brand,
                car.Model,
                car.Description,
                car.ImageUrl,
                car.CategoryId,
                car.Year,
                dealerId);

            return RedirectToAction(nameof(All));
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
        
        //[Authorize]
        //public IActionResult Edit(int id)
        //{

        //}
    }
}
