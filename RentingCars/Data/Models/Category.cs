using System.ComponentModel.DataAnnotations;

using static RentingCars.Data.DataConstants.Category;

namespace RentingCars.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Car> Cars { get; set; } = new List<Car>();
    }
}
