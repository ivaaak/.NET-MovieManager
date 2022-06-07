using FluentValidation;
using MovieManager.Models;

namespace MovieManager.Infrastructure.Validators
{
    public class SearchValidator : AbstractValidator<SearchResultViewModel>
    {
        public SearchValidator()
        {
            RuleFor(x => x.SearchTerm).NotNull().NotEmpty().WithMessage("Search key word cannot be empty");

            RuleFor(x => x.SearchTerm).MinimumLength(1).MaximumLength(200).WithMessage("Search key word has to be between 1 and 200 characters");
        }
    }
}
