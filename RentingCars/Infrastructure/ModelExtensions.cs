using RentingCars.Services.Cars.Models;

namespace RentingCars.Infrastructure
{
    public static class ModelExtensions
    {
        public static string GetInformation(this ICarModel car)
        {
            return car.Brand + "-" + car.Model + "-" + car.Year;
        }
    }
}
