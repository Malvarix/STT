using STT.Application.Clients.Implementations.Imdb.Models.Common;
using STT.Application.Clients.Implementations.Imdb.Models.Request;
using STT.Application.Clients.Interfaces;
using STT.Application.Dto.Request;
using STT.Application.Services.Interfaces;
using STT.Application.Clients.Implementations.Imdb.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace STT.Application.Services.Implementations
{
    public class FilmService : IFilmService
    {
        private readonly IImdbClient _imdbClient;

        public FilmService(IImdbClient imdbClient)
        {
            _imdbClient = imdbClient;
        }

        public async Task<SearchData> SearchFilmAsync(
            SearchFilmRequestDto searchFilmRequestDto, 
            CancellationToken cancellationToken)
        {
            var baseRequestModel = new BaseRequestModel(searchFilmRequestDto)
            {
                Endpoint = Endpoint.Search
            };

            return await _imdbClient.GetDataAsync<SearchData>(baseRequestModel, cancellationToken);
        }

        public async Task<PosterData> GetFilmPostersAsync(
            FilmIdRequestDto filmIdRequestDto, 
            CancellationToken cancellationToken)
        {
            var baseRequestModel = new BaseRequestModel(filmIdRequestDto)
            {
                Endpoint = Endpoint.Posters
            };

            return await _imdbClient.GetDataAsync<PosterData>(baseRequestModel, cancellationToken);
        }

        public async Task<RatingData> GetFilmRatingsAsync(
            FilmIdRequestDto filmIdRequestDto, 
            CancellationToken cancellationToken)
        {
            var baseRequestModel = new BaseRequestModel(filmIdRequestDto)
            {
                Endpoint = Endpoint.Ratings
            };

            return await _imdbClient.GetDataAsync<RatingData>(baseRequestModel, cancellationToken);
        }

        public async Task<WikipediaData> GetFilmDescriptionFromWikipedia(
            FilmIdRequestDto filmIdRequestDto, 
            CancellationToken cancellationToken)
        {
            var baseRequestModel = new BaseRequestModel(filmIdRequestDto)
            {
                Endpoint = Endpoint.Wikipedia
            };

            return await _imdbClient.GetDataAsync<WikipediaData>(baseRequestModel, cancellationToken);
        }
    }
}