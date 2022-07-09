using MovieManager.Infrastructure.Constants;
using System.ComponentModel.DataAnnotations;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Reviews;

namespace MovieManager.Core.ViewModels
{
    public class MovieCardViewModel
    {
        public MovieCardViewModel() { }

        public Movie Movie { get; set; }

        public List<Cast> MovieActorsList { get; set; }

        public List<ReviewBase> Reviews { get; set; }

        //Only used if there is a review form posted from the Review view
        [MaxLength(DataConstants.Review.TitleMaxLength)]
        [MinLength(DataConstants.Review.TitleMinLength)]
        public string ReviewTitle { get; set; }

        [MaxLength(DataConstants.Review.ContentMaxLength)]
        [MinLength(DataConstants.Review.ContentMinLength)]
        public string ReviewContent { get; set; }

        [Range(0,10)]
        public decimal Rating { get; set; }

        [MaxLength(DataConstants.Movie.TitleMaxLength)]
        [MinLength(DataConstants.Movie.TitleMinLength)]
        public string MovieTitle { get; set; }
        public int MovieId { get; set; }

    }
}
