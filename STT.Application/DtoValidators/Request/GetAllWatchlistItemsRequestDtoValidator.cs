using FluentValidation;
using STT.Application.Dto.Request;

namespace STT.Application.DtoValidators.Request
{
    public class GetAllWatchlistItemsRequestDtoValidator : AbstractValidator<GetAllWatchlistItemsRequestDto>
    {
        public GetAllWatchlistItemsRequestDtoValidator()
        {
            RuleFor(x => x.WatchlistId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}