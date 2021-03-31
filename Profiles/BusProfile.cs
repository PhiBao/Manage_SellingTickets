using AutoMapper;
using backend.Dtos;
using backend.Models;

namespace backend.Profiles
{
    public class BusProfile : Profile
    {
        public BusProfile()
        {
            CreateMap<BusUpdateDto, Xe>();
        }
    }
}