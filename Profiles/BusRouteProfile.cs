using AutoMapper;
using backend.Models;
using backend.Dtos;

namespace backend.Profiles
{
    public class BusRouteProfile: Profile
    {
        public BusRouteProfile()
        {
            CreateMap<BusRouteUpdateDto, Tuyenxe>();
        }
    }
}