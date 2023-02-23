using RentingCars.Data;
using RentingCars.Data.Models;
using RentingCars.Services.Dealers;
using RentingCars.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RentingCars.Tests.Services
{
    public class DealerServiceTest
    {
        [Fact]
        public void IsDealerShouldReturnTrueWhenUserIsDealer()
        {
            // Arange
            const string userId = "TestUserId";
            const string name = "TestName";
            const string phoneNumber = "1234567890";

            using var data = DatabaseMock.Instance;

            data.Dealers.Add(new Dealer
            {
                UserId = userId,
                Name = name,
                PhoneNumber = phoneNumber
            });
            data.SaveChanges();

            var dealerService = new DealerService(data);

            // Act
            var result = dealerService.IsDealer(userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsDealerShouldReturnFalseWhenUserIsNotDealer()
        {
            // Arange
            const string userId = "TestUserId";
            const string name = "TestName";
            const string phoneNumber = "1234567890";

            using var data = DatabaseMock.Instance;

            data.Dealers.Add(new Dealer
            {
                UserId = userId,
                Name = name,
                PhoneNumber = phoneNumber
            });
            data.SaveChanges();

            var dealerService = new DealerService(data);

            // Act
            var result = dealerService.IsDealer("SomeUserId");

            // Assert
            Assert.False(result);
        }

        
        //private static ApplicationDbContext TheData()
        //{
        //    const string userId = "TestUserId";
        //    const string name = "TestName";
        //    const string phoneNumber = "1234567890";

        //    using var data = DatabaseMock.Instance;

        //    data.Dealers.Add(new Dealer
        //    {
        //        UserId = userId,
        //        Name = name,
        //        PhoneNumber = phoneNumber
        //    });
        //    data.SaveChanges();

        //    return data;
        //}

    }
}
