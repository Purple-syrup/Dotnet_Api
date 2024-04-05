
using Dotnet_Api.Interfaces;
using Dotnet_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_Api.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dc;
        public UserRepository(DataContext dc)
        {
            _dc = dc;
            
        }
        public async Task<User> Authenticate(string userName, string password)
        {
            return await _dc.Users.FirstOrDefaultAsync(x=>x.Username==userName && x.Password==password);
        }

        public async void Register(string username, string password)
        {
            var user =new User{Username=username,Password=password};

            await _dc.Users.AddAsync(user);
        
        }
        
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users=await _dc.Users.ToListAsync();
            return users;
        }
    }
}