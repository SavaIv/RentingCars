using System.ComponentModel.DataAnnotations;

using static RentingCars.Data.DataConstants.Car;

namespace RentingCars.Data.Models
{
    public class Car
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(CarBrandMaxLength)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(CarModelMaxLength)]
        public string Model { get; set; }

        [Required]
        [MaxLength(CarDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        //[Range(CarYearMinValue, CarYearMaxValue)]
        public int Year { get; set; }

        public bool IsPublic { get; set; }

        public bool IsDeleted { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int DealerId { get; set; }

        public Dealer Dealer { get; set; }
    }
}
