
using System.ComponentModel.DataAnnotations;

namespace Dotnet_Api.Models
{
    public class User:BaseEntity
    {

        [Required]
        public required string Username { get; set; }

        [Required]
        public required byte[] Password { get; set; }
        public required byte[] PasswordKey { get; set; }
    }
}