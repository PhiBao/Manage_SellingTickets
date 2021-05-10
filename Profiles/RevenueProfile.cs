using AutoMapper;
using backend.Dtos;
using backend.Models;

namespace backend.Profiles
{
    public class RevenueProfile: Profile
    {
        public RevenueProfile()
        {
           CreateMap<RevenueCreateDto, Doanhthungay>();
           CreateMap<Doanhthungay, RevenueReadDto>();
        }
    }
}