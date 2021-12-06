using AutoMapper;
using STT.Application.Dto.Request;
using STT.Application.Dto.Response;
using STT.Application.Services.Interfaces;
using STT.Domain.Entities;
using STT.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace STT.Application.Services.Implementations
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IMapper _mapper;
        private readonly IWatchlistRepository _watchlistRepository;

        public WatchlistService(IMapper mapper, 
                                IWatchlistRepository watchlistRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _watchlistRepository = watchlistRepository ?? throw new ArgumentNullException(nameof(watchlistRepository));
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

            return await _watchlistRepository.CreateWatchlistItemAsync(watchlistItem, cancellationToken);
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

            return await _watchlistRepository.CreateWatchlistAsync(watchlist, cancellationToken);
        }

        public async Task<IEnumerable<GetWatchlistItemResponseDto>> GetAllWatchlistItemsAsync(
            GetAllWatchlistItemsRequestDto getAllWatchlistItemsRequestDto, 
            CancellationToken cancellationToken)
        {
            if (getAllWatchlistItemsRequestDto == null)
            {
                throw new ArgumentNullException(nameof(getAllWatchlistItemsRequestDto));
            }

            var watchlistItems = await _watchlistRepository.GetAllWatchlistItemsAsync(
                getAllWatchlistItemsRequestDto.WatchlistId, 
                getAllWatchlistItemsRequestDto.UserId, 
                cancellationToken);

            return _mapper.Map<IEnumerable<GetWatchlistItemResponseDto>>(watchlistItems);
        }

        public async Task<bool> UpdateWatchlistItemStateAsync(
            UpdateWatchlistItemIsWatchedRequestDto updateWatchlistItemIsWatchedRequestDto, 
            CancellationToken cancellationToken)
        {
            if (updateWatchlistItemIsWatchedRequestDto == null)
            {
                throw new ArgumentNullException(nameof(updateWatchlistItemIsWatchedRequestDto));
            }

            var watchlistItem = await _watchlistRepository.GetWatchlistItemAsync(updateWatchlistItemIsWatchedRequestDto.WatchlistItemId, cancellationToken);
            watchlistItem.IsWatched = updateWatchlistItemIsWatchedRequestDto.IsWatched; 

            return await _watchlistRepository.UpdateWatchlistItem(watchlistItem, cancellationToken);
        }
    }
}