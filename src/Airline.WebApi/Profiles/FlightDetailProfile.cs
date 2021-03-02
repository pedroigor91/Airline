using Airline.WebApi.Dtos;
using Airline.WebApi.Models;
using AutoMapper;

namespace Airline.WebApi.Profiles
{
    public class FlightDetailProfile : Profile
    {
        public FlightDetailProfile()
        {
            CreateMap<FlightDetailCreateDto, FlightDetail>();
            CreateMap<FlightDetailUpdateDto, FlightDetail>();
            CreateMap<FlightDetail, FlightDetailReadDto>();
        }
    }
}