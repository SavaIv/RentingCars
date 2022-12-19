using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

using static RentingCars.Data.DataConstants;

namespace RentingCars.Models.Cars
{
    public class AddCarFormModel
    {
        [Required]        
        [StringLength(
            CarBrandMaxLength, 
            MinimumLength = CarBrandMinLength, 
            ErrorMessage = "The Brand must be between 2 and 20 charcters")]
        public string Brand { get; set; }

        [Required]        
        [StringLength(
            CarModelMaxLength, 
            MinimumLength = CarModelMinLength, 
            ErrorMessage = "The Model must be between 2 and 30 charcters")]
        public string Model { get; set; }

        [Required]        
        [StringLength(
            CarDescriptionMaxLength, 
            MinimumLength = CarDescriptionMinLength, 
            ErrorMessage = "The Description must be between 6 and 160 charcters")]
        public string Description { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        [Range (CarYearMinValue, CarYearMaxValue)]
        public int Year { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [BindNever]
        public IEnumerable<CarCategoryViewModel> ? Categories { get; set; }
    }
}
