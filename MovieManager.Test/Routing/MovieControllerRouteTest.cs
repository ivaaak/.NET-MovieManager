using MovieManager.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace MovieManager.Test.Routing
{
    public class MovieControllerRouteTest
    {
        [Fact]
        public void MainRouteShouldBeMapped()
                    => MyRouting
                        .Configuration()
                        .ShouldMap("/")
                        .To<MovieController>(c => c.Main());
        //Search
        [Fact]
        public void SearchRouteShouldBeMapped()
                    => MyRouting
                        .Configuration()
                        .ShouldMap("/Movie/Search")
                        .To<MovieController>(c => c.Search());

        [Fact]
        public void SearchResultRouteShouldBeMapped()
                    => MyRouting
                        .Configuration()
                        .ShouldMap("/Movie/SearchResult")
                        .To<MovieController>(c => c.SearchResult(new Models.SearchResultViewModel()));
        
        //MovieCard
        [Fact]
        public void MovieCardShouldBeMapped()
                    => MyRouting
                        .Configuration()
                        .ShouldMap("/Movie/MovieCard")
                        .To<MovieController>(c => c.MovieCard(550));
        [Fact]
        public void ShowCardShouldBeMapped()
                    => MyRouting
                        .Configuration()
                        .ShouldMap("/Movie/ShowCard")
                        .To<MovieController>(c => c.ShowCard(63247));


        [Fact]
        public void ErrorRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Movie/Error")
                .To<MovieController>(c => c.Error());
    }
}
