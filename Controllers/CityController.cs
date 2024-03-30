using System.Linq;
using AutoMapper;
using Dotnet_Api.Dtos;
using Dotnet_Api.Interfaces;
using Dotnet_Api.Models;
using Microsoft.AspNetCore.Mvc;


namespace Dotnet_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CityController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetCities(){
            var cities = await _uow.CityRepository.GetCitiesAsync();
            // var citiesDto= cities.Select(x=>new CityDto{Id=x.Id, Name=x.Name});
            var citiesDto= _mapper.Map<IEnumerable<CityDto>>(cities);
            return Ok(citiesDto);
        }

      
        [HttpPost]
        [HttpPost("post")]
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            // var city= new City{Name=cityDto.Name, LastUpdatedBy=1, LastUpdatedOn=DateTime.UtcNow};
            var city = _mapper.Map<City>(cityDto);
            city.LastUpdatedBy=1; city.LastUpdatedOn=DateTime.UtcNow;
            _uow.CityRepository.AddCity(city);
            await _uow.SaveAsync();
            return StatusCode(201);
        }
        
        // [HttpPost("add")]
        // [HttpPost("add/{cityName}")]
        // public async Task<IActionResult> AddCity(string cityName)
        // {
        //     City city= new City(){Name=cityName};
        //     await _dc.Cities.AddAsync(city);
        //     await _dc.SaveChangesAsync();
        //     return Ok(city);
        // }

        [HttpDelete("delete/{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            _uow.CityRepository.DeleteCity(id);
        
            await _uow.SaveAsync();
            return Ok(id);
        }
    }
}