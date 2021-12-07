using STT.Application.Clients.Implementations.Imdb.Models.Common;
using STT.Application.Dto.Request;
using System.Threading;
using System.Threading.Tasks;

namespace STT.Application.Services.Interfaces
{
    public interface IFilmService
    {
        Task<SearchData> SearchFilmAsync(
            SearchFilmRequestDto searchFilmRequestDto,
            CancellationToken cancellationToken);

        Task<PosterData> GetFilmPostersAsync(
            FilmIdRequestDto filmIdRequestDto,
            CancellationToken cancellationToken);

        Task<RatingData> GetFilmRatingsAsync(
            FilmIdRequestDto filmIdRequestDto,
            CancellationToken cancellationToken);

        Task<WikipediaData> GetFilmDescriptionFromWikipedia(
            FilmIdRequestDto filmIdRequestDto,
            CancellationToken cancellationToken);
    }
}