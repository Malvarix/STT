using System;

namespace STT.Application.Dto.Request
{
    public class GetAllWatchlistItemsRequestDto
    {
        public Guid WatchlistId { get; set; } 
        public Guid UserId { get; set; }
    }
}