using AutoMapper;
using backend.Dtos;
using backend.Models;

namespace backend.Profiles
{
    public class SeatProfile : Profile
    {
        public SeatProfile()
        {
            CreateMap<SeatUpdateDto, Chongoi>();
            CreateMap<SeatCreateDto, Chongoi>();
            CreateMap<Chongoi, SeatReadDto>();
        }
    }
}