using AutoTrader.Models;
using AutoTrader.Models.Dtos;
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

        [HttpGet("GetById")]
        public ActionResult<Car> GetRecordById(int id)
        {
            using (CarDbContext context = new CarDbContext())
            {
                var car = context.Cars.FirstOrDefault(car => car.Id == id);

                if (car != null)
                {
                    return Ok(new {message = "Sikeres lekérdezés", result = car});
                }
                else
                {
                    return NotFound(new { message = "Nincs ilyen id!!!" });
                }

            }

        }

        [HttpDelete]
        public ActionResult<Car> DeleteARecordById(int id)
        {

            using (CarDbContext context = new CarDbContext())
            {
                var car = context.Cars.FirstOrDefault(car => car.Id == id);
                if (car != null) 
                {
                    context.Cars.Remove(car);
                    context.SaveChanges();
                    return Ok(new { message = "Sikeres törlés" });
                }
                else
                {
                    return NotFound(new { message = "Nincs ilyen id!!!" });
                }
                    
            }
        }

        [HttpPut]
        public ActionResult PutARecord(int id, UpdateCarDto updateCarDto) 
        {
            using (var context = new CarDbContext()) 
            {
                var existingCar = context.Cars.FirstOrDefault(car => car.Id == id);

                if (existingCar != null)
                {
                    existingCar.Brand = updateCarDto.Brand;
                    existingCar.Type = updateCarDto.Type;
                    existingCar.Color = updateCarDto.Color;
                    existingCar.Year = updateCarDto.Year;

                    context.Cars.Update(existingCar);
                    context.SaveChanges();

                    return Ok(new {message = "Sikeres frissítés"});
                }

                return NotFound(new { message = "Nincs ilyen id!!!"});
            }
        }

    }
}
