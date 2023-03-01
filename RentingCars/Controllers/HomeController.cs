using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RentingCars.Data;
using RentingCars.Models;
using RentingCars.Models.Cars;

using RentingCars.Services.Cars;
using RentingCars.Services.Cars.Models;
using RentingCars.Services.Statistics;
using System.Diagnostics;

namespace RentingCars.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService cars;        
        private readonly IMemoryCache cache;

        public HomeController(
            ICarService _cars,           
            IMemoryCache _cache)
        {
            cars = _cars;           
            cache = _cache;
        }

        public IActionResult Index()
        {
            const string latestCarsCacheKey = "latestCarsCacheKey";

            var latestCars = cache.Get<List<LatestCarServiceModel>>(latestCarsCacheKey);

            if (latestCars == null)
            {
                latestCars = cars
                    .Latest()
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                cache.Set(latestCarsCacheKey, latestCars, cacheOptions);
            }

            return View(latestCars);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}