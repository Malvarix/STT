using STT.Application.Clients.Implementations.Imdb.Models.Request;
using STT.Application.Clients.Implementations.Imdb.Models.Response;
using STT.Application.Clients.Interfaces;
using STT.Application.Services.Interfaces;
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

        public async Task<SearchResponseModel> GetFilmAsync(SearchRequestModel searchRequestModel, CancellationToken cancellationToken)
        {
            return await _imdbClient.GetFilmAsync(searchRequestModel, cancellationToken);
        }
    }
}