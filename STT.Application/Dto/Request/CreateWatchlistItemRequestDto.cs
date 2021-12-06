using System;

namespace STT.Application.Dto.Request
{
    public class CreateWatchlistItemRequestDto
    {
        public Guid WatchlistId { get; set; }
        public string? FilmId { get; set; }
        public bool IsWatched { get; set; }
    }
}