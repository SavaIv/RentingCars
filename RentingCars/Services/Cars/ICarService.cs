using RentingCars.Models;

namespace RentingCars.Services.Cars
{
    public interface ICarService
    {
        CarQueryServiceModel All(
            string brand,
            string searchTerm,
            CarSorting sorting,
            int currentPage,
            int carsPerPage);

        IEnumerable<CarServiceModel> ByUser(string iserId);

        IEnumerable<string> AllCarBrands();
    }
}
