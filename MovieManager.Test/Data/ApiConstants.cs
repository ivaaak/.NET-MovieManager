using System;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

namespace MovieManager.Test.Data
{
    public class ApiConstants
    {
        //api result movie
        public static SearchMovie apiMovie = new SearchMovie()
        {
            PosterPath = "/IfB9hy4JH1eH6HEfIgIGORXi5h.jpg",
            Adult = false,
            Overview = "Jack Reacher must uncover the truth behind a major government conspiracy in order to clear his name. On the run as a fugitive from the law, Reacher uncovers a potential secret from his past that could change his life forever.",
            ReleaseDate = new DateTime(2016, 10, 19),
            GenreIds = { 53, 28, 80, 18, 9648 },
            Id = 343611,
            OriginalTitle = "Jack Reacher: Never Go Back",
            OriginalLanguage = "en",
            Title = "Jack Reacher: Never Go Back",
            BackdropPath = "/4ynQYtSEuU5hyipcGkfD6ncwtwz.jpg",
            Popularity = 26.818468,
            VoteCount = 201,
            Video = false,
            VoteAverage = 4.19,
            MediaType = TMDbLib.Objects.General.MediaType.Tv,
        };
        //api result show
        public static SearchTv apiShow = new SearchTv()
        {
            PosterPath = "/IfB9hy4JH1eH6HEfIgIGORXi5h.jpg",
            Overview = "Jack Reacher must uncover the truth behind a major government conspiracy in order to clear his name. On the run as a fugitive from the law, Reacher uncovers a potential secret from his past that could change his life forever.",
            FirstAirDate = new DateTime(2016, 10, 19),
            GenreIds = { 53, 28, 80, 18, 9648 },
            Id = 343611,
            OriginalName = "Jack Reacher: Never Go Back",
            OriginalLanguage = "en",
            Name = "Jack Reacher: Never Go Back",
            BackdropPath = "/4ynQYtSEuU5hyipcGkfD6ncwtwz.jpg",
            Popularity = 26.818468,
            VoteCount = 201,
            VoteAverage = 4.19,
            MediaType = TMDbLib.Objects.General.MediaType.Tv,
        };

        public static TMDbLib.Objects.Movies.Movie apiMovieObject = new TMDbLib.Objects.Movies.Movie()
        {
            PosterPath = "/IfB9hy4JH1eH6HEfIgIGORXi5h.jpg",
            Adult = false,
            Overview = "Jack Reacher must uncover the truth behind a major government conspiracy in order to clear his name. On the run as a fugitive from the law, Reacher uncovers a potential secret from his past that could change his life forever.",
            ReleaseDate = new DateTime(2016, 10, 19),
            Id = 343611,
            OriginalTitle = "Jack Reacher: Never Go Back",
            OriginalLanguage = "en",
            Title = "Jack Reacher: Never Go Back",
            BackdropPath = "/4ynQYtSEuU5hyipcGkfD6ncwtwz.jpg",
            Popularity = 26.818468,
            VoteCount = 201,
            Video = false,
            VoteAverage = 4.19,
        };
        public static TvShow apiShowObject = new TvShow()
        {
            PosterPath = "/IfB9hy4JH1eH6HEfIgIGORXi5h.jpg",
            Overview = "Jack Reacher must uncover the truth behind a major government conspiracy in order to clear his name. On the run as a fugitive from the law, Reacher uncovers a potential secret from his past that could change his life forever.",
            FirstAirDate = new DateTime(2016, 10, 19),
            GenreIds = { 53, 28, 80, 18, 9648 },
            Id = 343611,
            OriginalName = "Jack Reacher: Never Go Back",
            OriginalLanguage = "en",
            Name = "Jack Reacher: Never Go Back",
            BackdropPath = "/4ynQYtSEuU5hyipcGkfD6ncwtwz.jpg",
            Popularity = 26.818468,
            VoteCount = 201,
            VoteAverage = 4.19,
        };
    }
}