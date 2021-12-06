using STT.Application.Dto.Request;
using STT.Application.Dto.Response;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace STT.Application.Services.Interfaces
{
    public interface IWatchlistService
    {
        Task<Guid> CreateWatchlistItemAsync(
            CreateWatchlistItemRequestDto createWatchlistItemRequestDto, 
            CancellationToken cancellationToken);

        Task<Guid> CreateWatchlistAsync(
            CreateWatchlistRequestDto createWatchlistRequestDto, 
            CancellationToken cancellationToken);

        Task<IEnumerable<GetWatchlistItemResponseDto>> GetAllWatchlistItemsAsync(
            GetAllWatchlistItemsRequestDto getAllWatchlistItemsRequestDto, 
            CancellationToken cancellationToken);

        Task<bool> UpdateWatchlistItemStateAsync(
            UpdateWatchlistItemIsWatchedRequestDto updateWatchlistItemIsWatchedRequestDto, 
            CancellationToken cancellationToken);
    }
}