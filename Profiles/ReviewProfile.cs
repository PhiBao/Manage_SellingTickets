using AutoMapper;
using backend.Models;
using backend.Dtos;

namespace backend.Profiles
{
    public class ReviewProfile: Profile
    {
        public ReviewProfile()
        {
            CreateMap<ReviewUpdateDto, Danhgia>();
            CreateMap<ReviewCreateDto, Danhgia>();

            CreateMap<Danhgia, ReviewReadDto>()
            .ForMember(dest => dest.TenNd, opt => opt.MapFrom(src => src.MaNdNavigation.TenNd));
        }
    }
}