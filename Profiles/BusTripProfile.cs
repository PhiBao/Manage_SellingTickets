using AutoMapper;
using backend.Models;
using backend.Dtos;
using System;

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
            .ForMember(dest => dest.NhaXe, opt => opt.MapFrom(src => src.MaXeNavigation.NhaXe))
            .ForMember(dest => dest.NgayDen, opt => opt.MapFrom(src => 
            new DateTime(src.NgayXuatBen.Year, src.NgayXuatBen.Month, 
               src.NgayXuatBen.Day + (src.NgayXuatBen.Hour + src.MaTuyenXeNavigation.ThoiGianDiChuyen.GetValueOrDefault()) / 24,
                (src.NgayXuatBen.Hour + src.MaTuyenXeNavigation.ThoiGianDiChuyen.GetValueOrDefault()) % 24,
                src.NgayXuatBen.Minute, src.NgayXuatBen.Second)
            ));
            
            CreateMap<BusTripUpdateDto, Chuyenxe>();
            CreateMap<BusTripCreateDto, Chuyenxe>();
        }
    }
}