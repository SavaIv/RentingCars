using AutoMapper;
using RentingCars.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCars.Tests.Mocks
{
    public static class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperConfiguraion = new MapperConfiguration(config =>
                {
                    config.AddProfile<MappingProfile>();
                });

                return new Mapper(mapperConfiguraion);
            }
        }
    }
}
