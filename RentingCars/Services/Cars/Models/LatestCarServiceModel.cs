namespace RentingCars.Services.Cars.Models
{
    public class LatestCarServiceModel : ICarModel
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public int Year { get; set; }
    }
}
