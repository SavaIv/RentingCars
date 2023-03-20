using AutoMapper;
using AutoMapper.QueryableExtensions;
using RentingCars.Data;
using RentingCars.Data.Models;
using RentingCars.Models;
using RentingCars.Services.Cars.Models;
//using static RentingCars.Data.DataConstants;

namespace RentingCars.Services.Cars
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public CarService(ApplicationDbContext _data, IMapper _mapper)
        {
            data = _data;
            mapper = _mapper;
        }

        public CarQueryServiceModel All(
            string brand = null,
            string searchTerm = null,
            CarSorting sorting = CarSorting.DateCreated,
            int currentPage = 1,
            int carsPerPage = int.MaxValue,
            bool publicOnly = true)
        {
            var carsQuery = data.Cars
                .Where(c => c.IsDeleted == false)
                .Where(c => publicOnly ? c.IsPublic : true)
                .AsQueryable();

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

        public IEnumerable<LatestCarServiceModel> Latest()
        {
            return data
                .Cars
                .Where(c => c.IsPublic)
                .OrderByDescending(c => c.Id)
                .ProjectTo<LatestCarServiceModel>(mapper.ConfigurationProvider)
                .Take(3)
                .ToList();
        }

        public bool Delete(int carId)
        {
            var carData = data.Cars.FirstOrDefault(c => c.Id == carId);

            if (carData == null)
            {
                return false;
            }

            //data.Cars.Remove(carData);
            carData.IsDeleted = true;
            data.SaveChanges();

            return true;
        }

        public CarDetailsServiceModel Details(int id)
        {
            return data
                    .Cars
                    .Where(c => c.Id == id)
                    .ProjectTo<CarDetailsServiceModel>(mapper.ConfigurationProvider)
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
                DealerId = dealerId,
                IsPublic = false
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
            int year,
            bool isPublic)
        {
            var carData = data.Cars.Find(id);

            if (carData == null)
            {
                return false;
            }

            carData.Brand = brand;
            carData.Model = model;
            carData.Description = description;
            carData.ImageUrl = imageUrl;
            carData.CategoryId = categoryId;
            carData.Year = year;
            carData.IsPublic = isPublic;

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

        public void ChangeVisibility(int carId)
        {
            var car = data.Cars.Find(carId);

            car.IsPublic = !car.IsPublic;

            data.SaveChanges();
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
                    .ProjectTo<CarCategoryServiceModel>(mapper.ConfigurationProvider)
                    .ToList();
        }

        public bool CategoryExists(int categoryId)
        {
            return data
                .Categories
                .Any(c => c.Id == categoryId);
        }

        private IEnumerable<CarServiceModel> GetCars(IQueryable<Car> carQuery)
        {
            return carQuery
                    .ProjectTo<CarServiceModel>(mapper.ConfigurationProvider)
                    .ToList();
        }

    }
}
