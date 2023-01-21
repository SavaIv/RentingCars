using Microsoft.AspNetCore.Mvc;
using RentingCars.Models;
using RentingCars.Models.Api.Cars;
using RentingCars.Services.Cars;

namespace RentingCars.Controllers.Api
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/cars")]
    public class CarsApiController : ControllerBase
    {
        private readonly ICarService cars;

        public CarsApiController(ICarService _cars)
        {
            cars = _cars;
        }

        [HttpGet]
        public CarQueryServiceModel All([FromQuery] AllCarsApiRequestModel query)
        {
            return cars.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.CarsPerPage);
        }

    }
}
