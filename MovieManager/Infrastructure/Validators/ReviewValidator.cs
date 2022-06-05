using FluentValidation;
using MovieManager.Models;

namespace MovieManager.Infrastructure.Validators
{
    public class ReviewValidator : AbstractValidator<ReviewViewModel>
    {
        public ReviewValidator()
        {
            RuleFor(x => x.ReviewId).NotNull().NotEmpty();

            RuleFor(x => x.ReviewTitle).NotNull().NotEmpty().WithMessage("Review title cannot be null");

            RuleFor(x => x.ReviewContent).NotNull().NotEmpty().WithMessage("Review Content cannot be null");
            RuleFor(x => x.ReviewContent).Length(10, 3000).WithMessage("Review Content has to be between 10 and 3000 characters long.");


            RuleFor(x => x.MovieTitle).NotNull().NotEmpty().WithMessage("Movie title cannot be null");

            RuleFor(x => x.Rating).NotNull().NotEmpty().WithMessage("Rating is required!");
            RuleFor(x => x.Rating).InclusiveBetween(1, 10).WithMessage("Rating has to be between 1 and 10");
            RuleFor(x => x.Rating).ScalePrecision(2, 4).WithMessage("Rating has to be a decimal number!"); 
            //Checks if it is the same type as the property
        }
    }
}
