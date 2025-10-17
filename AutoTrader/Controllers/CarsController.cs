using AutoTrader.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoTrader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Car> GetAllRecord() 
        {
            using (CarDbContext context = new CarDbContext())
            {
                var cars = context.Cars.ToList();

                if (cars != null)
                {
                    return Ok(cars);
                }

                return BadRequest(new { message = "Sikertelen lekérdezés" });
            }
            
        }

        [HttpPost]
        public ActionResult<Car> AddNewRecord(Car car)
        {
            using (CarDbContext context = new CarDbContext())
            {
                var newCar = new Car()
                {
                    Brand = car.Brand,
                    Type = car.Type,
                    Color = car.Color,
                    Year = car.Year
                };

                if (newCar != null)
                {
                    context.Cars.Add(newCar);
                    context.SaveChanges();
                    //return Ok(newCar);
                    return StatusCode(201, newCar);
                }
                else
                {
                    return BadRequest(new { message = "Sikertelen feltöltés" });

                }
            }
        }
    }
}
