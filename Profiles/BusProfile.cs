using AutoMapper;
using backend.Dtos;
using backend.Models;

namespace backend.Profiles
{
    public class BusProfile : Profile
    {
        public BusProfile()
        {
            CreateMap<BusUpdateDto, Xe>();

            CreateMap<Xe, BusReadDto>()
            .ForMember(dest => dest.TenNv, opt => opt.MapFrom(src => src.MaNvNavigation.TenNd))
            .ForMember(dest => dest.Sdt, opt => opt.MapFrom(src => src.MaNvNavigation.Sdt));
        }
    }
}