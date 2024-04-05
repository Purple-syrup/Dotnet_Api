
using Dotnet_Api.Data.Repo;
using Dotnet_Api.Interfaces;

namespace Dotnet_Api.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dc;

        public UnitOfWork(DataContext dc)
        {
            _dc = dc;
        }

        public ICityRepository CityRepository => new CityRepository(_dc);

        public IUserRepository UserRepository =>  new UserRepository(_dc);

        public async Task<bool> SaveAsync()
        {
            return await _dc.SaveChangesAsync() > 0;
        }
    }
}