using System.ComponentModel;

namespace STT.Application.Clients.Implementations.Imdb.Enums
{
    public enum Endpoint
    {
        [Description("Unknown or not defined endpoint.")]
        Unknown = 0,

        [Description("Search into all titles.")]
        Search = 1,

        [Description("Get Posters of Movie or Series TV.")]
        Posters = 2,

        [Description("Get ratings of Movie or Series TV in: IMDb, Metacritic, RottenTomatoes, TheMovieDb and TV.com.")]
        Ratings = 3,

        [Description("Get Wikipedia plot of Movie or Series TV as PlainText and Html.")]
        Wikipedia = 4,
    }
}