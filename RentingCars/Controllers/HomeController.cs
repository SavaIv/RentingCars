using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using RentingCars.Data;
using RentingCars.Models;
using RentingCars.Models.Cars;
using RentingCars.Models.Home;
using RentingCars.Services.Cars;
using RentingCars.Services.Statistics;
using System.Diagnostics;

namespace RentingCars.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService cars;
        private readonly IStatisticsService statistics;        

        public HomeController(
            ICarService _cars, 
            IStatisticsService _statistics)
        {
            cars = _cars;
            statistics = _statistics;                                    
        }

        public IActionResult Index()
        {
            var latestCars = cars.Latest().ToList();

            var totalStatistics = statistics.Total();

            return View(new IndexViewModel
            {
                TotalCars = totalStatistics.TotalCars,
                TotalUsers = totalStatistics.TotalUsers,
                Cars = latestCars
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