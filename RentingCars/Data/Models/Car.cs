using System.ComponentModel.DataAnnotations;

using static RentingCars.Data.DataConstants;

namespace RentingCars.Data.Models
{
    public class Car
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(CarBrandMaxlength)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(CarModelMaxlength)]
        public string Model { get; set; }

        [Required]
        [MaxLength(CarDescriptionlMaxlength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        //[Range(CarYearMinValue, CarYearMaxValue)]
        public int Year { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
