namespace RentingCars.Models.Api.Cars
{
    public class AllCarsApiResponseModel
    {
        public int CurrentPage { get; set; }

        public int TotalCars { get; set; }

        public IEnumerable<CarResponceModel> Cars { get; set; }
    }
}
