namespace STT.Application.Clients.Implementations.Imdb.Models.Common
{
    public enum SearchType
    {
        Title = 1,
        Movie = 2,
        Series = 4,
        Name = 8,
        Episode = 16,
        Company = 32,
        Keyword = 64,
        All = 128
    }
}