using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using RentingCars.Data;
using RentingCars.Models;
using RentingCars.Models.Cars;
using RentingCars.Models.Home;
using RentingCars.Services.Statistics;
using System.Diagnostics;

namespace RentingCars.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext data;

        public HomeController(IStatisticsService _statistics, 
            IMapper _mapper,
            ApplicationDbContext _data)
        {
            statistics = _statistics;
            mapper = _mapper;
            data = _data;            
        }

        public IActionResult Index()
        {
            var cars = data
                .Cars
                .OrderByDescending(c => c.Id)
                .ProjectTo<CarIndexViewModel>(mapper.ConfigurationProvider)                
                .Take(3)
                .ToList();

            var totalStatistics = statistics.Total();

            return View(new IndexViewModel
            {
                TotalCars = totalStatistics.TotalCars,
                TotalUsers = totalStatistics.TotalUsers,
                Cars = cars
            });
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