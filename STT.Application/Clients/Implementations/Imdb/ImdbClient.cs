using Microsoft.Extensions.Options;
using STT.Application.Clients.Implementations.Imdb.Models.Options;
using STT.Application.Clients.Implementations.Imdb.Models.Request;
using STT.Application.Clients.Interfaces;
using STT.Application.Clients.Implementations.Imdb.Enums;
using STT.Application.Helpers;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

namespace STT.Application.Clients.Implementations.Imdb
{
    public class ImdbClient : IImdbClient
    {
        private readonly IOptions<ImdbClientOptions> _options;
        private readonly IHttpClientFactory _httpClientFactory;

        public ImdbClient(IOptions<ImdbClientOptions> options, 
                          IHttpClientFactory httpClientFactory)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<T> GetDataAsync<T>(
            BaseRequestModel baseRequestModel, 
            CancellationToken cancellationToken)
        {
            if (baseRequestModel == null)
            {
                throw new ArgumentNullException(nameof(baseRequestModel));
            }

            var language = baseRequestModel.Language.GetDescription();

            string? endpointUri;
            switch (baseRequestModel.Endpoint)
            {
                case Endpoint.Search:
                    endpointUri = _options.Value.SearchEndpoint;
                    break;

                case Endpoint.Posters:
                    endpointUri = _options.Value.PostersEndpoint;
                    break;

                case Endpoint.Ratings:
                    endpointUri = _options.Value.RatingsEndpoint;
                    break;

                case Endpoint.Wikipedia:
                    endpointUri = _options.Value.WikipediaEndpoint;
                    break;

                default:
                    throw new InvalidOperationException("Endpoint is not supported for fetching data.");
            }

            var expression = baseRequestModel.Endpoint == Endpoint.Unknown || baseRequestModel.Endpoint == Endpoint.Search 
                ? baseRequestModel.Expression : baseRequestModel.Id;

            var fullUri = $"{_options.Value.Url}/{language}/{endpointUri}/{_options.Value.Key}/{expression}";

            HttpResponseMessage httpResponseMessage;

            using (var client = _httpClientFactory.CreateClient())
            {
                httpResponseMessage = await client.GetAsync(fullUri, cancellationToken);
            }

            httpResponseMessage.EnsureSuccessStatusCode();

            var respoonseDataModel = await JsonSerializer
                .DeserializeAsync<T>(
                    await httpResponseMessage.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true },
                    cancellationToken);

            return respoonseDataModel ?? throw new NullReferenceException(nameof(respoonseDataModel));
        }
    }
}