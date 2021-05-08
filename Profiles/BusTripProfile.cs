using AutoMapper;
using backend.Models;
using backend.Dtos;

namespace backend.Profiles
{
    public class BusTripProfile: Profile
    {
        public BusTripProfile()
        {
            CreateMap<Chuyenxe, BusTripReadDto>()
            .ForMember(dest => dest.TenBxDi, opt => opt.MapFrom(src => src.MaTuyenXeNavigation.MaBxdiNavigation.TenBx))
            .ForMember(dest => dest.TenBxDen, opt => opt.MapFrom(src => src.MaTuyenXeNavigation.MaBxdenNavigation.TenBx))
            .ForMember(dest => dest.DiaChiBxDi, opt => opt.MapFrom(src => src.MaTuyenXeNavigation.MaBxdiNavigation.DiaChi))
            .ForMember(dest => dest.DiaChiBxDen, opt => opt.MapFrom(src => src.MaTuyenXeNavigation.MaBxdenNavigation.DiaChi))
            .ForMember(dest => dest.NhaXe, opt => opt.MapFrom(src => src.MaXeNavigation.NhaXe));
            
            CreateMap<BusTripUpdateDto, Chuyenxe>();
        }
    }
}