using FluentValidation;
using MovieManager.Models;

namespace MovieManager.Infrastructure.Validators
{
    public class TrailerValidator : AbstractValidator<TrailerViewModel>
    {
        public TrailerValidator()
        {
            RuleFor(x => x.TrailerId).NotNull().NotEmpty();

            RuleFor(x => x.TrailerKey).NotNull().NotEmpty();
        }
    }
}
