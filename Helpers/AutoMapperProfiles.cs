
using AutoMapper;
using Dotnet_Api.Dtos;
using Dotnet_Api.Models;

namespace Dotnet_Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityDto>().ReverseMap();

        }
    }
}