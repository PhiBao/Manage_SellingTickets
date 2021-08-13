using AutoMapper;
using backend.Dtos;
using backend.Models;

namespace backend.Profiles
{
    public class GarageProfile: Profile
    {
        public GarageProfile()
        {
            CreateMap<Nhaxe, GarageReadDto>();
            CreateMap<GarageCreateDto, Nhaxe>();
        }
    }
}