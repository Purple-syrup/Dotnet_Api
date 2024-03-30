

namespace Dotnet_Api.Interfaces
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository {get;}

        Task<bool> SaveAsync();
    }
}