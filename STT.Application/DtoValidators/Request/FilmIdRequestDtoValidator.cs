using FluentValidation;
using STT.Application.Dto.Request;

namespace STT.Application.DtoValidators.Request
{
    public class FilmIdRequestDtoValidator : AbstractValidator<FilmIdRequestDto>
    {
        public FilmIdRequestDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .Must(x => !string.IsNullOrWhiteSpace(x) && x.StartsWith("tt"));
        }
    }
}