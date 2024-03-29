using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet_Api.Data;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetStrings(){
            var cities =_dc.Cities.ToList();
            return Ok(cities);
        }
    }
}