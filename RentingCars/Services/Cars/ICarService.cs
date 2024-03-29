﻿using RentingCars.Models;
using RentingCars.Services.Cars.Models;

namespace RentingCars.Services.Cars
{
    public interface ICarService
    {
        CarQueryServiceModel All(
            string brand = null,
            string searchTerm = null,
            CarSorting sorting = CarSorting.DateCreated,
            int currentPage = 1,
            int carsPerPage = int.MaxValue,
            bool publicOnly = true);

        IEnumerable<LatestCarServiceModel> Latest();

        bool Delete(int carId);

        CarDetailsServiceModel Details(int carId);

        int Create(
            string brand, 
            string model, 
            string description, 
            string imageUrl, 
            int categoryId, 
            int year, 
            int dealerId,
            decimal Price);

        bool Edit(
            int carId,
            string brand,
            string model,
            string description,
            string imageUrl,
            int categoryId,
            int year,
            bool isPublic);

        IEnumerable<CarServiceModel> ByUser(string userId);

        bool IsByDealer(int carId, int dealerId);

        void ChangeVisibility(int carId);

        IEnumerable<string> AllBrands();

        IEnumerable<CarCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);
    }
}
