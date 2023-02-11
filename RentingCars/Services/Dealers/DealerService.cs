using RentingCars.Data;

namespace RentingCars.Services.Dealers
{
    public class DealerService : IDealerService
    {
        private readonly ApplicationDbContext data;

        public DealerService(ApplicationDbContext _data)
        {
            data = _data;
        }

        public int IdbyUser(string userId)
        {
            return data
                .Dealers
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();
        }

        public bool IsDealer(string userId)
        {
            return data
                    .Dealers
                    .Any(d => d.UserId == userId);
        }
    }
}
