﻿using RentingCars.Data;
using RentingCars.Data.Models;
using RentingCars.Models;
//using static RentingCars.Data.DataConstants;

namespace RentingCars.Services.Cars
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext data;

        public CarService(ApplicationDbContext _data)
        {
            data = _data;
        }

        public CarQueryServiceModel All(
            string brand,
            string searchTerm,
            CarSorting sorting,
            int currentPage,
            int carsPerPage)
        {
            var carsQuery = data.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                carsQuery = carsQuery.Where(c => c.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                carsQuery = carsQuery.Where(c =>
                    c.Brand.ToLower().Contains(searchTerm.ToLower()) ||
                    c.Model.ToLower().Contains(searchTerm.ToLower()) ||
                    c.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            carsQuery = sorting switch
            {
                CarSorting.DateCreated => carsQuery.OrderByDescending(c => c.Id),
                CarSorting.Year => carsQuery.OrderByDescending(c => c.Year),
                CarSorting.BrandAndModel => carsQuery.OrderBy(c => c.Brand).ThenBy(c => c.Model),
                _ => carsQuery.OrderByDescending(c => c.Id)
            };

            var totalCars = carsQuery.Count();

            var cars = GetCars(carsQuery
                .Skip((currentPage - 1) * carsPerPage)
                .Take(carsPerPage));

            return new CarQueryServiceModel
            {
                TotalCars = totalCars,
                CurrentPage = currentPage,
                CarsPerPage = carsPerPage,
                Cars = cars
            };
        }

        public IEnumerable<CarServiceModel> ByUser(string userId)
        {
            return GetCars(data
                .Cars
                .Where(c => c.Dealer.UserId == userId));
        }

        public IEnumerable<string> AllCarBrands()
        {
            return data
                .Cars
                .Select(c => c.Brand)
                .OrderBy(br => br)
                .Distinct()
                .ToList();
        }

        private static IEnumerable<CarServiceModel> GetCars(IQueryable<Car> carQuery)
        {
            return carQuery
                    .Select(c => new CarServiceModel
                    {
                        Id = c.Id,
                        Brand = c.Brand,
                        Model = c.Model,
                        Year = c.Year,
                        ImageUrl = c.ImageUrl,
                        Category = c.Category.Name
                    })
                    .ToList();
        }
    }
}
