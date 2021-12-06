using FluentValidation;
using STT.Application.Dto.Request;

namespace STT.Application.DtoValidators.Request
{
    public class UpdateWatchlistItemIsWatchedRequestDtoValidator : AbstractValidator<UpdateWatchlistItemIsWatchedRequestDto>
    {
        public UpdateWatchlistItemIsWatchedRequestDtoValidator()
        {
            RuleFor(x => x.WatchlistItemId).NotEmpty();
        }
    }
}