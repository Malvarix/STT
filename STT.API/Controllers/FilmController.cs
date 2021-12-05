using Microsoft.AspNetCore.Mvc;
using STT.Application.Clients.Implementations.Imdb.Models.Request;
using STT.Application.Clients.Implementations.Imdb.Models.Response;
using STT.Application.Services.Interfaces;
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
        [Route(nameof(GetFilm))]
        public async Task<ActionResult<SearchResponseModel>> GetFilm([FromQuery] SearchRequestModel searchRequestModel)
        {
            return Ok(await _filmService.GetFilmAsync(searchRequestModel));
        }
    }
}