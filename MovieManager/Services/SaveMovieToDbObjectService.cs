using MovieManager.Data.DataModels;
using MovieManager.Services.ServicesContracts;
using TMDbLib.Objects.Search;

namespace MovieManager.Services
{
    public class SaveMovieToDbObjectService : ISaveMovieToDbObjectService
    {
        public SaveMovieToDbObjectService(){}


        public Movie MovieApiToObject(SearchMovie result)
        {
            if (result.Title == null || result.PosterPath == null || result.Overview == null) { return null; }

            Movie m = new Movie()
            {
                MovieId = result.Id,
                Title = result.Title,
                Overview = result.Overview,
                PosterUrl = BuildImageURL(result.PosterPath),
                Rating = (decimal)result.VoteAverage,
                ReleaseDate = result.ReleaseDate,
                MediaType = "movie",
            };
            return m;
        }


        public Movie ShowApiToObject(SearchTv result)
        {
            if (result.Name == null || result.PosterPath == null || result.Overview == null) { return null; }

            Movie m = new Movie()
            {
                MovieId = result.Id,
                Title = result.Name,
                Overview = result.Overview,
                PosterUrl = BuildImageURL(result.PosterPath),
                Rating = (decimal)result.VoteAverage,
                ReleaseDate = result.FirstAirDate,
                MediaType = "show",
            };
            return m;
        }



        public static string BuildImageURL(string resImageURL)
        {
            //url part returned from API example
            //string exImageURL = "/bQLrHIRNEkE3PdIWQrZHynQZazu.jpg";

            //string baseImageURL = "https://tmdb.mrunblock.bar/t/p/w600_and_h900_bestv2";
            //This has Hotlinking protection so it cant be loaded into the site

            string baseImageURL = "https://image.tmdb.org/t/p/w500";

            return baseImageURL + resImageURL;
            //this is a working link ready to be saved in db
        }
    }
}
