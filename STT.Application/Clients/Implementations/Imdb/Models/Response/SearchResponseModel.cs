using STT.Application.Clients.Implementations.Imdb.Models.Common;
using System.Collections.Generic;

namespace STT.Application.Clients.Implementations.Imdb.Models.Response
{
    public class SearchResponseModel
    {
        public string SearchType { get; set; }
        public string Expression { get; set; }
        public IEnumerable<SearchResult> Results { get; set; }
        public string ErrorMessage { get; set; }
    }
}