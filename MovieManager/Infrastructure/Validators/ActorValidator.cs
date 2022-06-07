using FluentValidation;
using MovieManager.Models;

namespace MovieManager.Infrastructure.Validators
{
    public class ActorValidator : AbstractValidator<ActorViewModel>
    {
        public ActorValidator()
        {
            RuleFor(x => x.Person.Name).NotNull().NotEmpty();

            RuleFor(x => x.Person.Id).NotNull().NotEmpty();

            RuleFor(x => x.Person.Biography).NotNull().NotEmpty();

            RuleFor(x => x.Person.ProfilePath).NotNull().NotEmpty();
        }
    }
}
