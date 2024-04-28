
using System.Security.Cryptography;
using System.Text;
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
            var user = await _dc.Users.FirstOrDefaultAsync(x => x.Username == userName);
            if (user == null)
                return null;
            if (!MatchPasswordHash(password, user.Password, user.PasswordKey))
                return null;

            return user;

        }

        private bool MatchPasswordHash(string password, byte[] passwordHash, byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512(key: passwordKey))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (hash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }

        public async void Register(string userName, string password)
        {
            byte[] passwordHash, passwordKey;

            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            }
            var user = new User
            {
                Username = userName,
                Password = passwordHash,
                PasswordKey = passwordKey
            };

            await _dc.Users.AddAsync(user);

        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _dc.Users.ToListAsync();
            return users;
        }

        public async Task<bool> UserAlreadyExists(string userName)
        {
            return await _dc.Users.AnyAsync(x => x.Username == userName);
        }
    }
}