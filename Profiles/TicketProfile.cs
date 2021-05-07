using AutoMapper;
using backend.Dtos;
using backend.Models;

namespace backend.Profiles
{
    public class TicketProfile: Profile
    {
        public TicketProfile()
        {
           CreateMap<Vexe, TicketReadDto>()
           .ForMember(dest => dest.BienSoXe, opt => opt.MapFrom(src => src.MaChuyenXeNavigation.MaXeNavigation.BienSoXe))
           .ForMember(dest => dest.TenTuyenXe, opt => opt.MapFrom(src => 
            src.MaChuyenXeNavigation.MaTuyenXeNavigation.MaBxdiNavigation.TenBx + " - " 
            + src.MaChuyenXeNavigation.MaTuyenXeNavigation.MaBxdenNavigation.TenBx))
           .ForMember(dest => dest.NgayXuatBen, opt => opt.MapFrom(src => src.MaChuyenXeNavigation.NgayXuatBen))
           .ForMember(dest => dest.DonGia, opt => opt.MapFrom(src => src.MaChuyenXeNavigation.DonGia));           
        }
    }
}