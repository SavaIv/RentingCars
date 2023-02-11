using RentingCars.Data;
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

        public CarDetailsServiceModel Details(int id)
        {
            return data
                    .Cars
                    .Where(c => c.Id == id)
                    .Select(c => new CarDetailsServiceModel
                    {
                        Id = c.Id,
                        Brand = c.Brand,
                        Model = c.Model,
                        Description = c.Description,
                        Year = c.Year,
                        ImageUrl = c.ImageUrl,
                        CategoryName = c.Category.Name,
                        DealerId = c.DealerId,
                        DealerName = c.Dealer.Name,
                        UserId = c.Dealer.UserId
                    })
                    .FirstOrDefault();
        }

        public int Create(string brand,
                            string model,
                            string description,
                            string imageUrl,
                            int categoryId,
                            int year,
                            int dealerId)
        {
            var theCar = new Car
            {
                Brand = brand,
                Model = model,
                Description = description,
                ImageUrl = imageUrl,
                CategoryId = categoryId,
                Year = year,
                DealerId = dealerId
            };

            data.Cars.Add(theCar);
            data.SaveChanges();

            return theCar.Id;
        }

        public bool Edit(
            int id,
            string brand,
            string model,
            string description,
            string imageUrl,
            int categoryId,
            int year)
        {
            var carData = data.Cars.Find(id);
                        
            if(carData == null)
            {
                return false;
            }

            carData.Brand = brand;
            carData.Model = model;
            carData.Description = description;
            carData.ImageUrl = imageUrl;
            carData.CategoryId = categoryId;
            carData.Year = year;            

            data.SaveChanges();

            return true;
        }

        public IEnumerable<CarServiceModel> ByUser(string userId)
        {
            return GetCars(data
                .Cars
                .Where(c => c.Dealer.UserId == userId));
        }

        public bool IsByDealer(int carId, int dealerId)
        {
            return data
                .Cars
                .Any(c => c.Id == carId && c.DealerId == dealerId);
        }

        public IEnumerable<string> AllBrands()
        {
            return data
                .Cars
                .Select(c => c.Brand)
                .OrderBy(br => br)
                .Distinct()
                .ToList();
        }

        public IEnumerable<CarCategoryServiceModel> AllCategories()
        {
            return data
                    .Categories
                    .Select(c => new CarCategoryServiceModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToList();
        }

        public bool CategoryExists(int categoryId)
        {
            return data
                .Categories
                .Any(c => c.Id == categoryId);
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
                        CategoryName = c.Category.Name
                    })
                    .ToList();
        }        
    }
}
