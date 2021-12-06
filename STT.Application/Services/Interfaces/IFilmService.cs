using STT.Application.Clients.Implementations.Imdb.Models.Request;
using STT.Application.Clients.Implementations.Imdb.Models.Response;
using System.Threading;
using System.Threading.Tasks;

namespace STT.Application.Services.Interfaces
{
    public interface IFilmService
    {
        Task<SearchResponseModel> GetFilmAsync(SearchRequestModel searchRequestModel, CancellationToken cancellationToken);
    }
}