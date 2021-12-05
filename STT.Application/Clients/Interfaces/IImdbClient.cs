using STT.Application.Clients.Implementations.Imdb.Models.Request;
using STT.Application.Clients.Implementations.Imdb.Models.Response;
using System.Threading.Tasks;

namespace STT.Application.Clients.Interfaces
{
    public interface IImdbClient
    {
        public string ApiKey { get; }

        public Task<SearchResponseModel> GetFilmAsync(SearchRequestModel searchRequestModel);
    }
}