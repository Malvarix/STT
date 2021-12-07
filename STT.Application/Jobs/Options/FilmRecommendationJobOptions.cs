using System;
using System.Net;

namespace STT.Application.Jobs.Options
{
    public class FilmRecommendationJobOptions
    {
        public Guid? WatchlistId { get; set; }
        public int? NotWatchedCountOfFilms { get; set; }
        public int? MonthFilmRecommendationsCountLimit { get; set; }
        public string? UserEmail { get; set; }
        public NetworkCredential? NetworkCredentials { get; set; }
    }
}