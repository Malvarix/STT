using Microsoft.EntityFrameworkCore;
using STT.Domain.Entities;
using STT.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace STT.Persistence.Repositories.Implementations
{
    public class WatchlistRepository : IWatchlistRepository
    {
        private readonly SttDbContext _context;

        public WatchlistRepository(SttDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> CreateWatchlistItemAsync(WatchlistItem watchlistItem, CancellationToken cancellationToken)
        {
            if (watchlistItem == null)
            {
                throw new ArgumentNullException(nameof(watchlistItem));
            }

            await _context.AddAsync(watchlistItem, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return watchlistItem.WatchlistItemId;
        }

        public async Task<Guid> CreateWatchlistAsync(Watchlist watchlist, CancellationToken cancellationToken)
        {
            if (watchlist == null)
            {
                throw new ArgumentNullException(nameof(watchlist));
            }

            await _context.AddAsync(watchlist, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return watchlist.WatchlistId;
        }

        public async Task<WatchlistItem> GetWatchlistItemAsync(Guid watchlistItemId, CancellationToken cancellationToken)
        {
            var watchlistItem = await _context.WatchlistItems.FirstOrDefaultAsync(i => i.WatchlistItemId == watchlistItemId, cancellationToken);
            if (watchlistItem == null)
            {
                throw new NullReferenceException(nameof(watchlistItem));
            }

            return watchlistItem;
        }

        public async Task<IEnumerable<WatchlistItem>?> GetAllWatchlistItemsAsync(Guid watchlistId, Guid userId, CancellationToken cancellationToken)
        {
            var watchlist = await _context.Watchlists
                .Include(w => w.WatchlistItems)
                .FirstOrDefaultAsync(w => w.WatchlistId == watchlistId && w.UserId == userId, cancellationToken);

            if (watchlist == null)
            {
                throw new NullReferenceException(nameof(watchlist));
            }

            return watchlist.WatchlistItems;
        }

        public async Task<bool> UpdateWatchlistItem(WatchlistItem watchlistItem, CancellationToken cancellationToken)
        {
            if (watchlistItem == null)
            {
                throw new ArgumentNullException(nameof(watchlistItem));
            }

            await Task.Run(async () =>
            {
                _context.Update(watchlistItem);
                await _context.SaveChangesAsync(cancellationToken);
            }, cancellationToken);

            return true;
        }
    }
}