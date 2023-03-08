using RentingCars.Data;

namespace RentingCars.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext data;

        public StatisticsService(ApplicationDbContext _data)
        {
            data = _data;
        }

        public StatisticsServiceModel Total()
        {
            var totalCars = data.Cars.Count(c => c.IsPublic);
            var totalUsers = data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalCars = totalCars,
                TotalUsers = totalUsers
            };
        }
    }
}
