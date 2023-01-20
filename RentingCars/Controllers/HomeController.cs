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
        private readonly ApplicationDbContext data;

        public HomeController(IStatisticsService _statistics, ApplicationDbContext _data)
        {
            statistics = _statistics;
            data = _data;
        }

        public IActionResult Index()
        {
            var cars = data
                .Cars
                .OrderByDescending(c => c.Id)
                .Select(c => new CarIndexViewModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    ImageUrl = c.ImageUrl
                })
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}