using AutoMapper;
using backend.Models;
using backend.Dtos;

namespace backend.Profiles
{
    public class BusTripProfile: Profile
    {
        public BusTripProfile()
        {
            CreateMap<Chuyenxe, BusTripReadDto>();
        }
    }
}