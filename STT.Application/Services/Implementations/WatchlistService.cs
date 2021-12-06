using AutoMapper;
using Microsoft.EntityFrameworkCore;
using STT.Application.Dto.Request;
using STT.Application.Dto.Response;
using STT.Application.Services.Interfaces;
using STT.Domain.Entities;
using STT.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace STT.Application.Services.Implementations
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IMapper _mapper;
        private readonly SttDbContext _context;

        public WatchlistService(IMapper mapper, 
                                SttDbContext sttDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = sttDbContext ?? throw new ArgumentNullException(nameof(_context));
        }

        public async Task<Guid> CreateWatchlistItemAsync(
            CreateWatchlistItemRequestDto createWatchlistItemRequestDto, 
            CancellationToken cancellationToken)
        {
            if (createWatchlistItemRequestDto == null)
            {
                throw new ArgumentNullException(nameof(createWatchlistItemRequestDto));
            }

            var watchlistItem = _mapper.Map<WatchlistItem>(createWatchlistItemRequestDto);

            await _context.AddAsync(watchlistItem, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return watchlistItem.WatchlistItemId;
        }

        public async Task<Guid> CreateWatchlistAsync(
            CreateWatchlistRequestDto createWatchlistRequestDto, 
            CancellationToken cancellationToken)
        {
            if (createWatchlistRequestDto == null)
            {
                throw new ArgumentNullException(nameof(createWatchlistRequestDto));
            }

            var watchlist = _mapper.Map<Watchlist>(createWatchlistRequestDto);

            await _context.AddAsync(watchlist, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return watchlist.WatchlistId;
        }

        public async Task<IEnumerable<GetWatchlistItemResponseDto>> GetAllWatchlistItemsAsync(
            GetAllWatchlistItemsRequestDto getAllWatchlistItemsRequestDto, 
            CancellationToken cancellationToken)
        {
            if (getAllWatchlistItemsRequestDto == null)
            {
                throw new ArgumentNullException(nameof(getAllWatchlistItemsRequestDto));
            }

            var watchlist = await _context.Watchlists
                .Include(i => i.WatchlistItems)
                .FirstOrDefaultAsync(i => i.WatchlistId == getAllWatchlistItemsRequestDto.WatchlistId 
                                            && i.UserId == getAllWatchlistItemsRequestDto.UserId, cancellationToken);

            if (watchlist == null)
            {
                throw new NullReferenceException(nameof(watchlist));
            }

            if (watchlist.WatchlistItems == null || !watchlist.WatchlistItems.Any())
            {
                return new List<GetWatchlistItemResponseDto>();
            }

            return _mapper.Map<IEnumerable<WatchlistItem>, IEnumerable<GetWatchlistItemResponseDto>>(watchlist.WatchlistItems);
        }

        public async Task<bool> UpdateWatchlistItemStateAsync(
            UpdateWatchlistItemIsWatchedRequestDto updateWatchlistItemIsWatchedRequestDto, 
            CancellationToken cancellationToken)
        {
            if (updateWatchlistItemIsWatchedRequestDto == null)
            {
                throw new ArgumentNullException(nameof(updateWatchlistItemIsWatchedRequestDto));
            }

            var watchlistItem = await _context.WatchlistItems
                .FirstOrDefaultAsync(i => i.WatchlistItemId == updateWatchlistItemIsWatchedRequestDto.WatchlistItemId, cancellationToken);

            if (watchlistItem == null)
            {
                throw new NullReferenceException(nameof(watchlistItem));
            }

            watchlistItem.IsWatched = updateWatchlistItemIsWatchedRequestDto.IsWatched;

            return await Task.Run(async () =>
            {
                _context.Update(watchlistItem);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }, cancellationToken);
        }
    }
}