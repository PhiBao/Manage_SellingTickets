using AutoMapper;
using backend.Dtos;
using backend.Models;

namespace backend.Profiles
{
    public class AccountProfile: Profile
    {
        public AccountProfile()
        {
           CreateMap<Taikhoan, AccountReadDto>().ForMember(dest => dest.MaNd, opt => opt.MapFrom(src => src.MaTk));
           CreateMap<AccountUpdateDto, Taikhoan>();
        }
    }
}