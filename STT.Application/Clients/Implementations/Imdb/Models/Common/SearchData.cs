using System.Collections.Generic;

namespace STT.Application.Clients.Implementations.Imdb.Models.Common
{
    public class SearchData
    {
        public string? SearchType { get; set; }
        public string? Expression { get; set; }
        public IEnumerable<SearchResult>? Results { get; set; }
        public string? ErrorMessage { get; set; }
    }
}