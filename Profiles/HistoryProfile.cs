using AutoMapper;
using backend.Dtos;
using backend.Models;

namespace backend.Profiles
{
    public class HistoryProfile : Profile
    {
        public HistoryProfile()
        {
            CreateMap<Lichsutimkiem, HistoryReadDto>();
            CreateMap<HistoryCreateDto, Lichsutimkiem>();
        }
    }
}