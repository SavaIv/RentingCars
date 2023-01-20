using Microsoft.AspNetCore.Mvc;
using RentingCars.Data;
using RentingCars.Models.Api.Statistics;
using RentingCars.Services.Statistics;

namespace RentingCars.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        public static IStatisticsService statistics;

        public StatisticsApiController(IStatisticsService _statistics)
        {
            statistics = _statistics;
        }

        [HttpGet]
        public StatisticsServiceModel GetStatistics()
        {           
            return statistics.Total();
        }


    }
}