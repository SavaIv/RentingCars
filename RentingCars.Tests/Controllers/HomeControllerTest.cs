using Microsoft.AspNetCore.Mvc;
using RentingCars.Controllers;
using RentingCars.Data.Models;
using RentingCars.Models.Home;
using RentingCars.Services.Cars;
using RentingCars.Services.Statistics;
using RentingCars.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RentingCars.Tests.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            // Arange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Cars.AddRange(Enumerable.Range(0, 10).Select(i => new Car()
            {
                Brand = "brand",
                Description = "description",
                ImageUrl = "imageUrl",
                Model = "model"                
            }));
            data.Users.Add(new User());
            data.SaveChanges();

            var carService = new CarService(data, mapper);
            var statisticService = new StatisticsService(data);

            var homeController = new HomeController(carService, statisticService);

            // Act
            var result = homeController.Index();

            // Assert
            Assert.NotNull(result);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;
            var indexViewModel = Assert.IsType<IndexViewModel>(model);

            Assert.Equal(3, indexViewModel.Cars.Count);
            Assert.Equal(10, indexViewModel.TotalCars);
            Assert.Equal(1, indexViewModel.TotalUsers);
        }


        [Fact]
        public void ErrorShouldReturnView()
        {
            // Arrange
            var homeController = new HomeController(null, null);

            // Act
            var result = homeController.Error();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
