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

            CreateMap<Tuyenxe, BusRouteReadDto>()
            .ForMember(dest => dest.TenTuyenXe, opt => opt.MapFrom(src => src.MaBxdiNavigation.TenBx + " - " + src.MaBxdenNavigation.TenBx))
            .ForMember(dest => dest.DiaChiBxDi, opt => opt.MapFrom(src => src.MaBxdiNavigation.DiaChi))
            .ForMember(dest => dest.DiaChiBxDen, opt => opt.MapFrom(src => src.MaBxdenNavigation.DiaChi));
        }
    }
}