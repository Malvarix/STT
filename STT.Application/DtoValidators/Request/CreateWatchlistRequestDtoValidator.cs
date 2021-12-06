using FluentValidation;
using STT.Application.Dto.Request;

namespace STT.Application.DtoValidators.Request
{
    public class CreateWatchlistRequestDtoValidator : AbstractValidator<CreateWatchlistRequestDto>
    {
        public CreateWatchlistRequestDtoValidator()
        {
            RuleFor(x => x.Title).MaximumLength(500).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}