
using System.ComponentModel.DataAnnotations;

namespace Dotnet_Api.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is Required")]
        [StringLength(50,MinimumLength =3)]
        [RegularExpression(".*[a-zA-Z]+.*",ErrorMessage ="Numerics not allowed")]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }
    }
}