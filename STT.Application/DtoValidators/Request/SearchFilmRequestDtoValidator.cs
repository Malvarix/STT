using FluentValidation;
using STT.Application.Dto.Request;

namespace STT.Application.DtoValidators.Request
{
    public class SearchFilmRequestDtoValidator : AbstractValidator<SearchFilmRequestDto>
    {
        public SearchFilmRequestDtoValidator()
        {
            RuleFor(x => x.Title).NotNull();
        }
    }
}