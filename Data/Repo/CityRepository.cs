using Dotnet_Api.Interfaces;
using Dotnet_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_Api.Data.Repo
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _dc;

        public CityRepository(DataContext dc)
        {
            _dc = dc;
        }
        public void AddCity(City city)
        {
            _dc.Cities.Add(city);
        }

        public void DeleteCity(int cityId)
        {
            var city= _dc.Cities.Find(cityId);
            _dc.Cities.Remove(city);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _dc.Cities.ToListAsync();
        }

        public async Task<City> FindCity(int cityId)
        {
            return await _dc.Cities.FindAsync(cityId);
            
        }
    }
}