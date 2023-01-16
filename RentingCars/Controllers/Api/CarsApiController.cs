using Microsoft.AspNetCore.Mvc;
using RentingCars.Data;
using System.Collections;

namespace RentingCars.Controllers.Api
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/cars")]
    public class CarsApiController : ControllerBase
    {
        private readonly ApplicationDbContext data;

        public CarsApiController(ApplicationDbContext _data)
        {
            data = _data;
        }

        [HttpGet]
        public IEnumerable NiakakvoIme()
        {
            return data.Cars.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public object DrugoIme(int id)
        {
            return data.Cars.Find(id);
        }

    }
}
