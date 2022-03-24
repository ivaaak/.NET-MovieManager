using MovieManager.Data.DataModels;
using MovieManager.Data;
using MovieManager.Services.ServicesContracts;
using System.Text;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using Microsoft.EntityFrameworkCore;
using TMDbLib.Client;
using MovieManager.Data.DBConfig;

namespace MovieManager.Services
{
    public class AddToDbService : IAddToDbService
    {
        private readonly MovieContext dataContext;

        private ISaveMovieToDbObjectService saveMovieFromApiToDbObject;

        private TMDbClient tmdbClient;

        public AddToDbService() { } //used for DI

        public AddToDbService(
            ISaveMovieToDbObjectService saveMovieToDbObjectService, 
            MovieContext data) 
        {
            this.saveMovieFromApiToDbObject = saveMovieToDbObjectService;
            this.dataContext = data;
            tmdbClient = new TMDbClient(Configuration.APIKey);
        }


        public void AddMovieToUserPlaylist(int movieId, string PlaylistName, string Name) //playlistid?
        {
            var movie = dataContext.Movies.Where(m => m.MovieId == movieId).FirstOrDefault();

            if(movie == null) //movie doesnt exist in db
            {
                var apiMovie = tmdbClient.GetMovieAsync(movieId).Result;        //get from api
                movie = saveMovieFromApiToDbObject.MovieApiToObject(apiMovie);  //turn to db object
                dataContext.Movies.Add(movie); //add to db
            };

            var targetPlaylist = dataContext.Playlists
                .Include(p => p.Movies)
                .Where(u => u.User.UserName == Name && u.PlaylistName == PlaylistName)
                .FirstOrDefault();

            if (!targetPlaylist.Movies.Contains(movie))
            {
                targetPlaylist.Movies.Add(movie);
            }

            dataContext.SaveChanges();

            Console.WriteLine($"Added Movie {movieId} to user - {Name}'s list: {PlaylistName}");
        }

        public void AddShowToUserPlaylist(int movieId, string PlaylistName, string Name)
        {
            var apiMovie = tmdbClient.GetTvShowAsync(movieId).Result;       //get from api
            var movie = saveMovieFromApiToDbObject.ShowApiToObject(apiMovie);

            if (dataContext.Movies.Where(m => m.MovieId == movieId).FirstOrDefault() == null) //movie doesnt exist in db
            {
                dataContext.Movies.Add(movie); //add to db
            };
            

            var targetPlaylist = dataContext.Playlists
                .Include(p => p.Movies)
                .Where(u => u.User.UserName == Name && u.PlaylistName == PlaylistName)
                .FirstOrDefault();

            if (!targetPlaylist.Movies.Contains(movie))
            {
                targetPlaylist.Movies.Add(movie);
            }

            dataContext.SaveChanges();

            Console.WriteLine($"Added Show {movieId} to user - {Name}'s list: {PlaylistName}");
        }

        public void AddMovieToFavorites(int movieId, string Name) //playlistid?
        {
            var movie = dataContext.Movies.Where(m => m.MovieId == movieId).FirstOrDefault();

            if (movie == null) //movie doesnt exist in db
            {
                var apiMovie = tmdbClient.GetMovieAsync(movieId).Result;        //get from api
                movie = saveMovieFromApiToDbObject.MovieApiToObject(apiMovie);  //turn to db object
                dataContext.Movies.Add(movie); //add to db
            };

            var targetPlaylist = dataContext.Playlists
                .Include(p => p.Movies)
                .Where(u => u.User.UserName == Name && u.PlaylistName == "favorites")
                .FirstOrDefault();
           
            if(targetPlaylist == null)
            {
                targetPlaylist = new Playlist()
                {
                    PlaylistName = "favorites",
                    CreatedOn = DateTime.Now,
                };
                dataContext.Playlists.Add(targetPlaylist);
                dataContext.Users
                    .Where(u => u.UserName == Name)
                    .FirstOrDefault()
                    .Playlists.Add(targetPlaylist); //fug
            }

            if (!targetPlaylist.Movies.Contains(movie))
            {
                targetPlaylist.Movies.Add(movie);
            }

            dataContext.SaveChanges();

            Console.WriteLine($"Added Movie {movieId} to user - {Name}'s favorites list");
        }





        public string AddMovies(SearchContainer<SearchMovie> results)
        {
            StringBuilder sb = new StringBuilder();
            List<Movie> validMovies = new List<Movie>();

            foreach (SearchMovie result in results.Results)
            {
                if(dataContext.Movies.Where(i => i.MovieId == result.Id).Any())
                {
                    Console.WriteLine($"{result.Title} is already added to watched movies.");
                    continue; // check so nothing is added twice
                }
                var m = this.saveMovieFromApiToDbObject.SearchMovieApiToObject(result);

                if(m != null)
                {
                    validMovies.Add(m);
                }
                sb.AppendLine($"Successfully added : {result.Title} to watched movies!");
            }

            dataContext.Movies.AddRange(validMovies);
            dataContext.SaveChanges();

            Console.WriteLine(sb.ToString());
            return sb.ToString().Trim();
        }
        

        public string AddShows(SearchContainer<SearchTv> results)
        {
            StringBuilder sb = new StringBuilder();
            List<Movie> validShows = new List<Movie>();

            foreach (SearchTv result in results.Results)
            {
                if(dataContext.Movies.Where(i => i.MovieId == result.Id).Any()) 
                {
                    Console.WriteLine($"{result.Name} is already added to movies.");
                    continue;
                }

                var m = this.saveMovieFromApiToDbObject.SearchShowApiToObject(result);

                if(m != null) { validShows.Add(m); }

                sb.AppendLine($"Successfully added : {result.Name} to show watchlist.");
            }

            dataContext.Movies.AddRange(validShows);
            dataContext.SaveChanges();

            Console.WriteLine(sb.ToString());
            return sb.ToString().Trim();
        }


        public string AddMovie(SearchMovie movie)
        {
            StringBuilder sb = new StringBuilder();
            Console.WriteLine(movie.Title);

            if (dataContext.Movies.Any(i => i.MovieId == movie.Id))
            {
                sb.Append($"{movie.Title} is already added to movies.");
            }
            else
            {
                var m = this.saveMovieFromApiToDbObject.SearchMovieApiToObject(movie);

                sb.Append($"Successfully added : {movie.Title} to movies!");

                dataContext.Movies.Add(m);
                dataContext.SaveChanges();
            }

            return sb.ToString();
        }    
    }
}
