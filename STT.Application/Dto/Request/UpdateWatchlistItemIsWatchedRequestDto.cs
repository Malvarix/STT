using System;

namespace STT.Application.Dto.Request
{
    public class UpdateWatchlistItemIsWatchedRequestDto
    {
        public Guid WatchlistItemId { get; set; }
        public bool IsWatched { get; set; }
    }
}