using AutoMapper;
using backend.Dtos;
using backend.Models;

namespace backend.Profiles
{
    public class BusStationProfile: Profile
    {
        public BusStationProfile()
        {
            CreateMap<Benxe, BusStationReadDto>();
            CreateMap<BusStationCreateDto, Benxe>();
        }
    }
}