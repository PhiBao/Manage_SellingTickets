using AutoMapper;
using backend.Dtos;
using backend.Models;

namespace backend.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserUpdateDto, Nguoidung>();

            CreateMap<Nguoidung, UserReadDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.MaNdNavigation.Email));
        }
    }
}