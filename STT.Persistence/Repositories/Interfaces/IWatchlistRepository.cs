using STT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace STT.Persistence.Repositories.Interfaces
{
    public interface IWatchlistRepository
    {
        Task<Guid> CreateWatchlistItemAsync(WatchlistItem watchlistItem, CancellationToken cancellationToken);
        Task<Guid> CreateWatchlistAsync(Watchlist watchlist, CancellationToken cancellationToken);
        Task<WatchlistItem> GetWatchlistItemAsync(Guid watchlistItemId, CancellationToken cancellationToken);
        Task<IEnumerable<WatchlistItem>?> GetAllWatchlistItemsAsync(Guid watchlistId, Guid userId, CancellationToken cancellationToken);
        Task<bool> UpdateWatchlistItem(WatchlistItem watchlistItem, CancellationToken cancellationToken);
    }
}