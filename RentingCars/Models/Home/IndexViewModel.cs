using RentingCars.Services.Cars.Models;

namespace RentingCars.Models.Home
{
    public class IndexViewModel
    {
        public int TotalCars { get; set; }

        public int TotalUsers { get; set; }

        public int TotalRents { get; set; }

        public IList<LatestCarServiceModel> Cars { get; set; }
    }
}
