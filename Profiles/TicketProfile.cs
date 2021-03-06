using System.Linq;
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
           .ForMember(dest => dest.MaNhaXe, opt => opt.MapFrom(src => src.MaChuyenXeNavigation.MaXeNavigation.MaNhaXe))
           .ForMember(dest => dest.BienSoXe, opt => opt.MapFrom(src => src.MaChuyenXeNavigation.MaXeNavigation.BienSoXe))
           .ForMember(dest => dest.TenTuyenXe, opt => opt.MapFrom(src => 
            src.MaChuyenXeNavigation.MaTuyenXeNavigation.MaBxdiNavigation.TenBx + " - " 
            + src.MaChuyenXeNavigation.MaTuyenXeNavigation.MaBxdenNavigation.TenBx))
           .ForMember(dest => dest.DonGia, opt => opt.MapFrom(src => src.MaChuyenXeNavigation.DonGia))
           .ForMember(dest => dest.TenKh, opt => opt.MapFrom(src => src.MaKhNavigation.TenNd))
           .ForMember(dest => dest.Sdt, opt => opt.MapFrom(src => src.MaKhNavigation.Sdt))
           .ForMember(dest => dest.DanhGia, opt => opt.MapFrom(src => src.Danhgias.Count() != 0));

           CreateMap<TicketCreateDto, Vexe>();       
           CreateMap<TicketUpdateDto, Vexe>();    
        }
    }
}