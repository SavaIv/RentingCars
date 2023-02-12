using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

using static RentingCars.Data.DataConstants.User;

namespace RentingCars.Data.Models
{
    public class User : IdentityUser
    {
        [MaxLength(FullNameMaxLength)]
        public string? FullName { get; set; }
    }
}
