using AutoMapper;
using RentingCars.Data.Models;
using RentingCars.Models.Cars;
using RentingCars.Models.Home;
using RentingCars.Services.Cars.Models;

namespace RentingCars.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, LatestCarServiceModel>();
            CreateMap<CarDetailsServiceModel, CarFormModel>();

            CreateMap<Car, CarDetailsServiceModel>()
                .ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Dealer.UserId));
        }
    }
}
