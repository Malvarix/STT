using Microsoft.AspNetCore.Mvc;
using STT.Application.Clients.Implementations.Imdb.Models.Common;
using STT.Application.Dto.Request;
using STT.Application.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace STT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _filmService;

        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        [HttpGet]
        [Route(nameof(SearchFilm))]
        public async Task<ActionResult<SearchData>> SearchFilm(
            [FromQuery] SearchFilmRequestDto searchFilmRequestDto, 
            CancellationToken cancellationToken)
        {
            return Ok(await _filmService.SearchFilmAsync(searchFilmRequestDto, cancellationToken));
        }
    }
}