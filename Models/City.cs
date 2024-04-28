
using System.ComponentModel.DataAnnotations;

namespace Dotnet_Api.Models
{
    public class City
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        public int LastUpdatedBy { get; set; }
    }
}