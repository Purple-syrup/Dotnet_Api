
using System.ComponentModel.DataAnnotations;

namespace Dotnet_Api.Models
{
    public class City:BaseEntity
    {


        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Country { get; set; }


    }
}