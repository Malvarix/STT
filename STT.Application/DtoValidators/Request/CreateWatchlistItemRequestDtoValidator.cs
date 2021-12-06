using FluentValidation;
using STT.Application.Dto.Request;

namespace STT.Application.DtoValidators.Request
{
    public class CreateWatchlistItemRequestDtoValidator : AbstractValidator<CreateWatchlistItemRequestDto>
    {
        public CreateWatchlistItemRequestDtoValidator()
        {
            RuleFor(x => x.WatchlistId).NotEmpty();
            RuleFor(x => x.FilmId).MaximumLength(500).NotEmpty();
        }
    }
}