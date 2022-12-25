using System.ComponentModel.DataAnnotations;

namespace RentingCars.Models.Cars
{
    public class AllCarsQueryModel
    {
        public IEnumerable<string> Brands { get; set; }

        [Display(Name = "Search")]
        public string SearchTetrm { get; set; }

        public CarSorting Sorting { get; set; }

        public IEnumerable<CarListingViewModel> Cars { get; set; }
    }
}
