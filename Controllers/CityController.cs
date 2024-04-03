using AutoMapper;
using Dotnet_Api.Dtos;
using Dotnet_Api.Interfaces;
using Dotnet_Api.Models;
using Microsoft.AspNetCore.JsonPatch;
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
            throw new UnauthorizedAccessException();
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

        [HttpPut("update/{Id:int}")]
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> UpdateCity(int Id,CityDto cityDto)
        {
            // try{

            if(Id != cityDto.Id)
            {
                return BadRequest("Update Not Allowed");
            }
            var dbCity=await _uow.CityRepository.FindCity(Id);
            if(dbCity ==null){
                return BadRequest("Update Not Allowed");
            }
            dbCity.LastUpdatedBy=1;dbCity.LastUpdatedOn=DateTime.UtcNow;
            _mapper.Map(cityDto,dbCity);

            throw new Exception("Unknown Error Occured");
            await _uow.SaveAsync();
            return StatusCode(200); 
            // }catch {
            //     return StatusCode(500,"Unknown Error Occured");
            // }
        }

        [HttpPatch("update/{id:int}")]
        [HttpPatch("{Id:int}")]
        public async Task<IActionResult> UpdateCityPatch(int Id,JsonPatchDocument<City> city)
        {
            var dbCity=await _uow.CityRepository.FindCity(Id);
            dbCity.LastUpdatedBy=1;dbCity.LastUpdatedOn=DateTime.UtcNow;
            city.ApplyTo(dbCity,ModelState);
            await _uow.SaveAsync();
            return StatusCode(200); 
        }

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