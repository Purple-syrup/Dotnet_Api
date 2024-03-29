using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet_Api.Data;
using Dotnet_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly DataContext _dc;

        public CityController(DataContext dc)
        {
            _dc = dc;
        }


        [HttpGet]
        public async Task<IActionResult> GetCities(){
            var cities = await  _dc.Cities.ToListAsync();
            return Ok(cities);
        }

      
        [HttpPost("post")]
        public async Task<IActionResult> AddCity(City city)
        {
            // City city= new City(){Name=cityName};
            await _dc.Cities.AddAsync(city);
            await _dc.SaveChangesAsync();
            return Ok(city);
        }
        
        [HttpPost("add")]
        [HttpPost("add/{cityName}")]
        public async Task<IActionResult> AddCity(string cityName)
        {
            City city= new City(){Name=cityName};
            await _dc.Cities.AddAsync(city);
            await _dc.SaveChangesAsync();
            return Ok(city);
        }

        [HttpDelete("delete/{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city= await _dc.Cities.FindAsync(id);
            _dc.Cities.Remove(city);
            await _dc.SaveChangesAsync();
            return Ok(city);
        }
    }
}