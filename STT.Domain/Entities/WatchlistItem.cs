using System;

namespace STT.Domain.Entities
{
    public class WatchlistItem
    {
        public Guid WatchlistItemId { get; set; }
        public Guid WatchlistId { get; set; }
        public string? FilmId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsWatched { get; set; }
        public byte MonthRecommendationsCount { get; set; }

        public Watchlist? Watchlist { get; set; }
    }
}