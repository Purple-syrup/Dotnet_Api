using Dotnet_Api.Models;

namespace Dotnet_Api.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();

        void AddCity(City city);

        void DeleteCity(int cityId);

    }
}