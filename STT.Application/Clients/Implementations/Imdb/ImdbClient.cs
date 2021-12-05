using Newtonsoft.Json;
using STT.Application.Clients.Implementations.Imdb.Models.Request;
using STT.Application.Clients.Implementations.Imdb.Models.Response;
using STT.Application.Clients.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace STT.Application.Clients.Implementations.Imdb
{
    public class ImdbClient : IImdbClient
    {
        public const string ApiUrl = "https://imdb-api.com";

        public string ApiKey { get; }

        public ImdbClient(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey));
            }

            ApiKey = apiKey;
        }

        public async Task<SearchResponseModel> GetFilmAsync(SearchRequestModel searchRequestModel)
        {
            const string SearchEndpointPath = "API/Search";

            if (searchRequestModel == null)
            {
                throw new ArgumentException(nameof(searchRequestModel));
            }

            var uri = $"{ApiUrl}/{SearchEndpointPath}/{ApiKey}/{searchRequestModel.Title}" 
                + (searchRequestModel.Year != null ? $" {searchRequestModel.Year}" : string.Empty);

            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(uri),
                Method = HttpMethod.Get
            };

            var httpResponseMessage = await new HttpClient().SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var film = JsonConvert.DeserializeObject<SearchResponseModel>(await httpResponseMessage.Content.ReadAsStringAsync());

            if (film == null)
            {
                throw new NullReferenceException(nameof(film));
            }

            return film;
        }
    }
}