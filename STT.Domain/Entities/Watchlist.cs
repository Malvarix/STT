using System;
using System.Collections.Generic;

namespace STT.Domain.Entities
{
    public class Watchlist
    {
        public Guid WatchlistId { get; set; }
        public string? Title { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public Guid UserId { get; set; }

        public ICollection<WatchlistItem>? WatchlistItems { get; set; }
    }
}