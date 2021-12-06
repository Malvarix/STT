using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using STT.Application.Clients.Implementations.Imdb.Models.Options;
using STT.Application.Clients.Implementations.Imdb.Models.Request;
using STT.Application.Clients.Implementations.Imdb.Models.Response;
using STT.Application.Clients.Interfaces;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace STT.Application.Clients.Implementations.Imdb
{
    public class ImdbClient : IImdbClient
    {
        private readonly IOptions<ImdbClientOptions> _options;
        private readonly IHttpClientFactory _httpClientFactory;

        public ImdbClient(IOptions<ImdbClientOptions> options, IHttpClientFactory httpClientFactory)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<SearchResponseModel> GetFilmAsync(SearchRequestModel searchRequestModel, CancellationToken cancellationToken)
        {
            const string SearchEndpointPath = "API/Search";

            if (searchRequestModel == null)
            {
                throw new ArgumentException(nameof(searchRequestModel));
            }

            var uri = $"{_options.Value.Url}/{SearchEndpointPath}/{_options.Value.Key}/{searchRequestModel.Title}" 
                + (searchRequestModel.Year != null ? $" {searchRequestModel.Year}" : string.Empty);

            HttpResponseMessage httpResponseMessage;

            using (var client = _httpClientFactory.CreateClient())
            {
                httpResponseMessage = await client.GetAsync(uri, cancellationToken);
            }

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