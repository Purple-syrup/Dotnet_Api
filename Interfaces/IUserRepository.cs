
using Dotnet_Api.Models;

namespace Dotnet_Api.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string userName, string password);

        void Register(string username, string password);

        Task<bool> UserAlreadyExists(string userName);

        Task<IEnumerable<User>> GetUsers();


    }
}