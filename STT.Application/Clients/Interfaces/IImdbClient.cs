using STT.Application.Clients.Implementations.Imdb.Models.Request;
using System.Threading;
using System.Threading.Tasks;

namespace STT.Application.Clients.Interfaces
{
    public interface IImdbClient
    {
        Task<T> GetDataAsync<T>(BaseRequestModel baseRequestModel, CancellationToken cancellationToken);
    }
}