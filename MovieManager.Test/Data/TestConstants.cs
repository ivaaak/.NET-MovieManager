using MovieManager.Data.DataModels;
using System.Collections.Generic;
using System.Linq;
using TMDbLib.Objects.Search;

namespace MovieManager.Test.Data
{
    public class TestConstants
    {
        public TestConstants()
        {
            movieList.Add(movie);
            userPlaylist.Movies.Add(movie);
            userPlaylist.PlaylistMovies.Add(playlistMovie);
        }
        //Data constant objects to be used in all tests
        public static Playlist playlist = new Playlist()
        {
            PlaylistName = "current",
            PlaylistId = "aaaaaa"
        };
        //db movie
        public static Movie movie = new Movie()
        {
            MovieId = 31414,
            Title = "Satantango",
            Overview = "Inhabitants of a small village in Hungary deal with the effects of the fall of Communism.",
            PosterUrl = "/y38z0v4HJ12MHiKddLEoFlvPiBt.jpg",
            Rating = 8.3m,
            Popularity = 11.68m,
            Language = "hu",
            MediaType = "movie",
        };
        public static PlaylistMovie playlistMovie = new PlaylistMovie()
        {
            Movie = movie,
            MovieId = movie.MovieId,
            Playlist = playlist,
            PlaylistId = playlist.PlaylistId
        };
        public static List<Movie> movieList = new List<Movie>();
        
        //api result movie
        public static SearchMovie searchMovie = new SearchMovie()
        {
            Id = 31414,
            Title = "Satantango",
            Overview = "Inhabitants of a small village in Hungary deal with the effects of the fall of Communism.",
            PosterPath = "/y38z0v4HJ12MHiKddLEoFlvPiBt.jpg",
            VoteAverage = 8.3,
            Popularity = 11.68,
            OriginalLanguage = "hu",
        };

        public static Actor actor = new Actor() 
        {
            ActorId = 111,
            FullName = "Test Actor",
            Overview = "An actor."
        };

        public static User user = new User()
        {
            UserName = "testUser",
            Id = "abcde",
        };
        public static Playlist userPlaylist = new Playlist()
        {
            PlaylistId = "aaaaa",
            PlaylistName = "playlist name",
            PlaylistMovies = new List<PlaylistMovie>(),
            Movies = new List<Movie>()
        };
        


        //unused atm
        public static IEnumerable<Movie> moviesCollection
            => Enumerable.Range(0, 100).Select(i => new Movie
            {
                MediaType = "movie"
            });

        public static IEnumerable<Movie> showsCollection
            => Enumerable.Range(0, 100).Select(i => new Movie
            {
                MediaType = "show"
            });
    }
}
