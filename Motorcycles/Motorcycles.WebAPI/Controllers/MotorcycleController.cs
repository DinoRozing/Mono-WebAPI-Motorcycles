using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Motorcycles.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MotorcycleController : ControllerBase
    {
        private static List<Motorcycle> motorcycles = new List<Motorcycle>
        {
            new Motorcycle { Make = "Honda", Model = "Cbr600rr", Year = 2011 },
            new Motorcycle { Make = "Yamaha", Model = "MT-07", Year = 2019 },
            new Motorcycle { Make = "Kawasaki", Model = "Ninja-zx6r", Year = 2021 }
        };

        [HttpPost("Add")]
        public ActionResult<Motorcycle> Post([FromBody] Motorcycle motorcycle)
        {
            if (motorcycle == null || !ModelState.IsValid)
            {
                return BadRequest("Dodavanje motora neuspješno!");
            }

            motorcycles.Add(motorcycle);
            return CreatedAtAction(nameof(Get), new { make = motorcycle.Make, model = motorcycle.Model, year = motorcycle.Year }, motorcycle);
        }

        [HttpGet("List")]
        public ActionResult<Motorcycle> Get(string make, string model, int year)
        {
            make = make.ToLower();
            model = model.ToLower();

            foreach (var motorcycle in motorcycles)
            {
                if (motorcycle.Make?.ToLower() == make && motorcycle.Model?.ToLower() == model && motorcycle.Year == year)
                {
                    return Ok(motorcycle);
                }
            }
            return NotFound("Lista nije pronađena!");
        }

        [HttpPut("Update")]
        public ActionResult Put(string make, string model, int year, [FromBody] Motorcycle updatedMotorcycle)
        {
            make = make.ToLower();
            model = model.ToLower();

            foreach (var motorcycle in motorcycles)
            {
                if (motorcycle.Make?.ToLower() == make && motorcycle.Model?.ToLower() == model && motorcycle.Year == year)
                {
                    if (updatedMotorcycle == null || !ModelState.IsValid)
                    {
                        return BadRequest("Neuspjelo ažuriranje!");
                    }

                    motorcycle.Make = updatedMotorcycle.Make;
                    motorcycle.Model = updatedMotorcycle.Model;
                    motorcycle.Year = updatedMotorcycle.Year;

                    return Ok("Motor je uspješno ažuriran!");
                }
            }
            return NotFound();
        }

        [HttpDelete("Remove")]
        public ActionResult Delete(string make, string model, int year)
        {
            make = make.ToLower();
            model = model.ToLower();

            foreach (var motorcycle in motorcycles)
            {
                if (motorcycle.Make?.ToLower() == make && motorcycle.Model?.ToLower() == model && motorcycle.Year == year)
                {
                    motorcycles.Remove(motorcycle);
                    return Ok("Motor je obrisan!");
                }
            }
            return NotFound("Neuspjelo brisanje!");
        }
    }

    public class Motorcycle
    {
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
    }
}
