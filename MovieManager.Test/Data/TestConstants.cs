using MovieManager.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

namespace MovieManager.Test.Data
{
    public class TestConstants
    {
        //Data constant objects to be used in all tests
        public static User user = new User()
        {
            UserName = "testUser",
            Id = "abcde",
            Email = "test@test.com",
            Playlists = new List<Playlist>(),
            Actors = new List<Actor>(),
        };
        public static Playlist playlist = new Playlist()
        {
            PlaylistName = "favorites",
            PlaylistId = "aaaaaa",
            PlaylistMovies = new List<PlaylistMovie>(),
            CreatedOn = DateTime.UtcNow,
            Movies = new List<Movie>(),
            User = user,
            IsPublic = true,
            QrCode = new QRCodeObject()
            {
                Id = "sdasdasfasfas",
                QrCodeImage = "the image bytes",
                TextContent = "movie - id"

            },
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
            ReleaseDate = DateTime.UtcNow,
            Playlists = new List<Playlist>(),
            PlaylistMovies = new List<PlaylistMovie>(),
        };

        public static PlaylistMovie playlistMovie = new PlaylistMovie()
        {
            Movie = movie,
            MovieId = movie.MovieId,
            Playlist = playlist,
            PlaylistId = playlist.PlaylistId,
        };

        public static Playlist userPlaylist = new Playlist()
        {
            PlaylistId = "aaaaa",
            PlaylistName = "playlist name",
            PlaylistMovies = new List<PlaylistMovie>(),
            Movies = new List<Movie>(),
            CreatedOn = DateTime.UtcNow,
            IsPublic = true,
            QrCode = new QRCodeObject()
            {
                Id = "sdasdasfasfasaaa",
                QrCodeImage = "the image bytes",
                TextContent = "movie - id"

            },
            User = new User(),
        };
        public static List<Movie> movieList = new List<Movie>();

        //api results
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
            Overview = "An actor.",
            PhotoUrl = "linkToThePhoto"

        };
    }
}