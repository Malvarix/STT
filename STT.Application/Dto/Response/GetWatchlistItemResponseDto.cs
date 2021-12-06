using System;

namespace STT.Application.Dto.Response
{
    public class GetWatchlistItemResponseDto
    {
        public Guid WatchlistItemId { get; set; }
        public Guid WatchlistId { get; set; }
        public string? FilmId { get; set; }
        public bool IsWatched { get; set; }
    }
}