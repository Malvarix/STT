using AutoMapper;
using STT.Application.Dto.Request;
using STT.Domain.Entities;

namespace STT.Application.AutoMapperProfiles
{
    public class DtoToEntityMappingProfile : Profile
    {
        public DtoToEntityMappingProfile()
        {
            CreateMap<CreateWatchlistRequestDto, Watchlist>();
            CreateMap<CreateWatchlistItemRequestDto, WatchlistItem>();
        }
    }
}