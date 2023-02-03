namespace RentingCars.Services.Dealers
{
    public interface IDealerService
    {
        public bool IsDealer(string userId);

        public int GetIdbyUser(string userId);
    }
}
