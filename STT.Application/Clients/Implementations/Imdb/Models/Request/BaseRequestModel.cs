using STT.Application.Clients.Implementations.Imdb.Enums;
using STT.Application.Dto.Request;
using System;

namespace STT.Application.Clients.Implementations.Imdb.Models.Request
{
    public class BaseRequestModel
    {
        public Language Language { get; set; }
        public Endpoint Endpoint { get; set; }
        public string? Expression { get; set; }
        public string? Id { get; set; }

        public BaseRequestModel(SearchFilmRequestDto searchFilmRequestDto)
        {
            if (searchFilmRequestDto == null)
            {
                throw new ArgumentNullException(nameof(searchFilmRequestDto));
            }

            Expression = $"{searchFilmRequestDto.Title}" + (searchFilmRequestDto.Year == null ? string.Empty : $" {searchFilmRequestDto.Year}");
        }

        public BaseRequestModel(FilmIdRequestDto filmIdRequestDto)
        {
            if (filmIdRequestDto == null)
            {
                throw new ArgumentNullException(nameof(filmIdRequestDto));
            }

            Id = filmIdRequestDto.Id;
        }
    }
}