﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AggregationService.Models;

namespace AggregationService.Controllers
{
    [Produces("application/json")]
    [Route("api/Drivers")]
    public class DriversController : Controller
    {
        private readonly DriversDbContext _context;

        public DriversController(DriversDbContext context)
        {
            _context = context;
        }

        // GET: api/Drivers
        [HttpGet]
        public IEnumerable<Driver> GetDrivers()
        {
            return _context.Drivers;
        }

        // GET: api/Drivers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDriver([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var driver = await _context.Drivers.SingleOrDefaultAsync(m => m.Id == id);

            if (driver == null)
            {
                return NotFound();
            }

            return Ok(driver);
        }

        [HttpGet("users/{userid}/cars")]
        public async Task<IActionResult> GetUserCars([FromRoute] int userid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Car> cars = new List<Car>();

            await _context.Drivers.ForEachAsync(d => 
            {
                if (d.IdUser == userid)
                {
                    foreach (Car c in _context.Cars)
                        if (d.IdCar == c.Id) cars.Add(c);
                }
            });

            if (cars == null)
            {
                return NotFound();
            }

            return Ok(cars);
        }

        [HttpGet("cars/{carid}/users")]
        public async Task<IActionResult> GetCarUsers([FromRoute] int carid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<User> users = new List<User>();

            await _context.Drivers.ForEachAsync(d =>
            {
                if (d.IdCar == carid)
                {
                    foreach (User u in _context.Users)
                        if (d.IdUser == u.Id) users.Add(u);
                }
            });

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // PUT: api/Drivers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriver([FromRoute] int id, [FromBody] Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != driver.Id)
            {
                return BadRequest();
            }

            _context.Entry(driver).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Drivers
        [HttpPost]
        public async Task<IActionResult> PostDriver([FromBody] Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDriver", new { id = driver.Id }, driver);
        }

        // DELETE: api/Drivers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var driver = await _context.Drivers.SingleOrDefaultAsync(m => m.Id == id);
            if (driver == null)
            {
                return NotFound();
            }

            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();

            return Ok(driver);
        }

        private bool DriverExists(int id)
        {
            return _context.Drivers.Any(e => e.Id == id);
        }
    }
}