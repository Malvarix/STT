using AutoMapper;
using STT.Application.Dto.Response;
using STT.Domain.Entities;

namespace STT.Application.AutoMapperProfiles
{
    public class EntityToDtoMappingProfile : Profile
    {
        public EntityToDtoMappingProfile()
        {
            CreateMap<WatchlistItem, GetWatchlistItemResponseDto>();
        }
    }
}