using STT.Application.Clients.Implementations.Imdb.Models.Request;
using STT.Application.Clients.Implementations.Imdb.Models.Response;
using System.Threading.Tasks;

namespace STT.Application.Services.Interfaces
{
    public interface IFilmService
    {
        public Task<SearchResponseModel> GetFilmAsync(SearchRequestModel searchRequestModel);
    }
}