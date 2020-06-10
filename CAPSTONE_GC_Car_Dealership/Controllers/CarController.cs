using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPSTONE_GC_Car_Dealership.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CAPSTONE_GC_Car_Dealership.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarDbContext _context;
        public CarController(CarDbContext context)
        {
            _context = context;
        }
        //GET: api/Car
        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetCar()
        {
            var cars = await _context.Car.ToListAsync();
            return cars;
        }

        //GET: api/Car/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                //returns a 404 error code if an employee with the given
                //id does not exist in the database
                return NotFound();
            }
            else
            {
                return car;
            }
        }
        //POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<Car>> AddCar(Car newCar)
        {
            if (ModelState.IsValid)
            {
                _context.Car.Add(newCar);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCar), new { id = newCar.Id }, newCar);
                //returns HTTP 201 status code - standard response for HTTP Post methods that create new
                //resources on the server
                //nameof(GetEmployee) - adds a location to the response, specifies the URI 
                //of the newly created employee (AKA where we can access the new employee)
                //C# "nameof" is used to avoid hard-coding the action in the CreatedAtAction call
            }
            else
            {
                return BadRequest();
            }
        }
    }
}