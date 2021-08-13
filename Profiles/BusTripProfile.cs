using AutoMapper;
using backend.Models;
using backend.Dtos;
using System;
using System.Globalization;

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
            .ForMember(dest => dest.TenNhaXe, opt => opt.MapFrom(src => src.MaXeNavigation.MaNhaXeNavigation.TenNhaXe))
            .ForMember(dest => dest.ThoiGianDiChuyen, opt => opt.MapFrom(src => src.MaTuyenXeNavigation.ThoiGianDiChuyen))
            .ForMember(dest => dest.NgayXuatBen, opt => opt.MapFrom(src => Array.ConvertAll(src.LichTrinh.ToCharArray(), c => MappingDate(c, src.GioXuatBen))));

            CreateMap<Chuyenxe, BusTripSearchDto>()
            .ForMember(dest => dest.TenBxDi, opt => opt.MapFrom(src => src.MaTuyenXeNavigation.MaBxdiNavigation.TenBx))
            .ForMember(dest => dest.TenBxDen, opt => opt.MapFrom(src => src.MaTuyenXeNavigation.MaBxdenNavigation.TenBx))
            .ForMember(dest => dest.DiaChiBxDi, opt => opt.MapFrom(src => src.MaTuyenXeNavigation.MaBxdiNavigation.DiaChi))
            .ForMember(dest => dest.DiaChiBxDen, opt => opt.MapFrom(src => src.MaTuyenXeNavigation.MaBxdenNavigation.DiaChi))
            .ForMember(dest => dest.TenNhaXe, opt => opt.MapFrom(src => src.MaXeNavigation.MaNhaXeNavigation.TenNhaXe))
            .ForMember(dest => dest.ThoiGianDiChuyen, opt => opt.MapFrom(src => src.MaTuyenXeNavigation.ThoiGianDiChuyen));
            
            CreateMap<BusTripUpdateDto, Chuyenxe>();
            CreateMap<BusTripCreateDto, Chuyenxe>();
        }

        private string MappingDate(char s, string time) {
            var number = (int)Char.GetNumericValue(s);
            DateTime today = DateTime.Today;
            DateTime dayOfSchedule = (number > (int)today.DayOfWeek) ? today.AddDays(number - (int)today.DayOfWeek)
                                                                : today.AddDays(7 + number - (int)today.DayOfWeek);                  
        
            return dayOfSchedule.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + "T" + time;
        }
    }
}