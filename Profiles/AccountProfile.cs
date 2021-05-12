using AutoMapper;
using backend.Dtos;
using backend.Models;

namespace backend.Profiles
{
    public class AccountProfile: Profile
    {
        public AccountProfile()
        {
           CreateMap<Taikhoan, AccountReadDto>()
           .ForMember(dest => dest.MaNd, opt => opt.MapFrom(src => src.MaTk))
           .ForMember(dest => dest.TenNd, opt => opt.MapFrom(src => src.Nguoidung.TenNd))
           .ForMember(dest => dest.Sdt, opt => opt.MapFrom(src => src.Nguoidung.Sdt))
           .ForMember(dest => dest.Cmnd, opt => opt.MapFrom(src => src.Nguoidung.Cmnd))
           .ForMember(dest => dest.DiaChi, opt => opt.MapFrom(src => src.Nguoidung.DiaChi))
           .ForMember(dest => dest.NgaySinh, opt => opt.MapFrom(src => src.Nguoidung.NgaySinh))
           .ForMember(dest => dest.Vaitro, opt => opt.MapFrom(src => src.Nguoidung.Vaitro));
           
           CreateMap<AccountUpdateDto, Taikhoan>();
           CreateMap<Taikhoan, AccountUpdateDto>();
           CreateMap<AccountCreateDto, Taikhoan>();
           CreateMap<Taikhoan, AccountCreateDto>();
        }
    }
}