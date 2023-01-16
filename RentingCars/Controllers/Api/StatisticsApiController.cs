using Microsoft.AspNetCore.Mvc;
using RentingCars.Data;
using RentingCars.Models.Api.Statistics;

namespace RentingCars.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        public static ApplicationDbContext data;

        public StatisticsApiController(ApplicationDbContext _data)
        {
            data = _data;
        }

        [HttpGet]
        public StatisticsResponseModel GetStatistics()
        {
            var totalCars = data.Cars.Count();
            var totalUsers = data.Users.Count();

            var statistics = new StatisticsResponseModel
            {
                TotalCars = totalCars,
                TotalUsers = totalUsers,
                TotalRents = 0
            };

            return statistics;
        }


    }
}
