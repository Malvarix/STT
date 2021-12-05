using FluentValidation;
using STT.Application.Clients.Implementations.Imdb.Models.Request;

namespace STT.Application.Clients.Implementations.Imdb.Validators
{
    public class SearchRequestModelValidator : AbstractValidator<SearchRequestModel>
    {
        public SearchRequestModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}