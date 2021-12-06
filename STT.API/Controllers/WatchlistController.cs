using Microsoft.AspNetCore.Mvc;
using STT.Application.Dto.Request;
using STT.Application.Dto.Response;
using STT.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace STT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistController : ControllerBase
    {
        private readonly IWatchlistService _watchlistService;

        public WatchlistController(IWatchlistService watchlistService)
        {
            _watchlistService = watchlistService;
        }

        [HttpPost]
        [Route(nameof(CreateWatchlist))]
        public async Task<ActionResult<Guid>> CreateWatchlist(
            CreateWatchlistRequestDto createWatchlistRequestDto, 
            CancellationToken cancellationToken)
        {
            return Ok(await _watchlistService.CreateWatchlistAsync(createWatchlistRequestDto, cancellationToken));
        }

        [HttpPost]
        [Route(nameof(CreateWatchlistItem))]
        public async Task<ActionResult<Guid>> CreateWatchlistItem(
            CreateWatchlistItemRequestDto createWatchlistItemRequestDto, 
            CancellationToken cancellationToken)
        {
            return Ok(await _watchlistService.CreateWatchlistItemAsync(createWatchlistItemRequestDto, cancellationToken));
        }

        [HttpGet]
        [Route(nameof(GetAllWatchlistItems))]
        public async Task<ActionResult<IEnumerable<GetWatchlistItemResponseDto>>> GetAllWatchlistItems(
            [FromQuery] GetAllWatchlistItemsRequestDto getAllWatchlistItemsRequestDto, 
            CancellationToken cancellationToken)
        {
            return Ok(await _watchlistService.GetAllWatchlistItemsAsync(getAllWatchlistItemsRequestDto, cancellationToken));
        }

        [HttpPatch]
        [Route(nameof(UpdateWatchlistItemState))]
        public async Task<ActionResult<bool>> UpdateWatchlistItemState(
            UpdateWatchlistItemIsWatchedRequestDto updateWatchlistItemIsWatchedRequestDto, 
            CancellationToken cancellationToken)
        {
            return Ok(await _watchlistService.UpdateWatchlistItemStateAsync(updateWatchlistItemIsWatchedRequestDto, cancellationToken));
        }
    }
}