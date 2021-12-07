using System.Collections.Generic;

namespace STT.Application.Clients.Implementations.Imdb.Models.Common
{
    public class PosterData
    {
        public string? IMDbId { get; set; }
        public string? Title { get; set; }
        public string? FullTitle { get; set; }
        public string? Type { set; get; }
        public string? Year { set; get; }

        public IEnumerable<PosterDataItem>? Posters { get; set; }
        public IEnumerable<PosterDataItem>? Backdors { get; set; }

        public string? ErrorMessage { get; set; }
    }
}