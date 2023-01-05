using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

using static RentingCars.Data.DataConstants.Dealer;

namespace RentingCars.Data.Models
{
    public class Dealer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DealerNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DealerPhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public IEnumerable<Car> Cars { get; set; } = new List<Car>();
    }
}
