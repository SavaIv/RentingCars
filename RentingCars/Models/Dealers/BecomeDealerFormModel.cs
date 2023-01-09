using System.ComponentModel.DataAnnotations;

using static RentingCars.Data.DataConstants.Dealer;

namespace RentingCars.Models.Dealers;

public class BecomeDealerFormModel
{
    [Required]
    [StringLength(DealerNameMaxLength, MinimumLength = DealerNameMinLength)]
    public string Name { get; set; }

    [Required]
    [StringLength(DealerPhoneNumberMaxLength, MinimumLength = DealerPhoneNumberMinLength)]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }
}
