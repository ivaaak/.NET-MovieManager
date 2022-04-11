using MovieManager.Data.DataModels;
using MovieManager.Services.ServicesContracts;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

namespace MovieManager.Services
{
    public class SaveMovieToDbObjectService : ISaveMovieToDbObjectService
    {
        public Movie SearchMovieApiToObject(SearchMovie result)
        {
            if (result.Title == null || result.PosterPath == null || result.Overview == null) 
            {
                return null;
            }
            Movie m = new Movie()
            {
                MovieId = result.Id,
                Title = result.Title,
                Overview = result.Overview,
                PosterUrl = BuildImageURL(result.PosterPath),
                Rating = (decimal)result.VoteAverage,
                ReleaseDate = result.ReleaseDate,
                MediaType = "movie",
                Language = result.OriginalLanguage,
            };
            return m;
        }

        public Movie SearchShowApiToObject(SearchTv result)
        {
            if (result.Name == null || result.PosterPath == null || result.Overview == null)
            {
                return null;
            }
            Movie m = new Movie()
            {
                MovieId = result.Id,
                Title = result.Name,
                Overview = result.Overview,
                PosterUrl = BuildImageURL(result.PosterPath),
                Rating = (decimal)result.VoteAverage,
                ReleaseDate = result.FirstAirDate,
                MediaType = "show",
                Language = result.OriginalLanguage,
            };
            return m;
        }

        //save to db
        public Movie MovieApiToObject(TMDbLib.Objects.Movies.Movie result)
        {
            if (result.Title == null || result.PosterPath == null || result.Overview == null)
            {
                return null;
            }
            string trailerLink = SearchMethodsService.GetMovieTrailerStatic(result.Id).Result;

            Movie m = new Movie()
            {
                MovieId = result.Id,
                Title = result.Title,
                Overview = result.Overview,
                PosterUrl = BuildImageURL(result.PosterPath),
                Rating = (decimal)result.VoteAverage,
                ReleaseDate = result.ReleaseDate,
                MediaType = "movie",
                TrailerLink = trailerLink,
                Language = result.OriginalLanguage,
            };
            return m;
        }

        public Movie ShowApiToObject(TvShow result)
        {
            if (result.Name == null || result.PosterPath == null || result.Overview == null)
            {
                return null;
            }
            string trailerLink = SearchMethodsService.GetShowTrailerStatic(result.Id).ToString();

            Movie m = new Movie()
            {
                MovieId = result.Id,
                Title = result.Name,
                Overview = result.Overview,
                PosterUrl = BuildImageURL(result.PosterPath),
                Rating = (decimal)result.VoteAverage,
                ReleaseDate = result.FirstAirDate,
                MediaType = "show",
                TrailerLink = trailerLink,
                Language = result.OriginalLanguage,
            };
            return m;
        }


        public Actor ActorApiToObject(TMDbLib.Objects.People.Person result)
        {
            if (result.Name == null || result.Images==null)
            {
                return null;
            }

            Actor a = new Actor()
            {
                ActorId = result.Id,
                FullName = result.Name,
                CountryCode = result.PlaceOfBirth,
                Overview = result.Biography,
            };
            return a;
        }


        public static string BuildImageURL(string resImageURL)
        {
            string baseImageURL = "https://image.tmdb.org/t/p/w500";

            return baseImageURL + resImageURL;
        }
    }
}
