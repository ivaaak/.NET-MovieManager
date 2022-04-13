using MovieManager.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace MovieManager.Test.Controllers
{

    //This isnt updated for ASP.NET Core 6 :()
    //System.InvalidOperationException :
    //This version of MyTested.AspNetCore.Mvc only supports ASP.NET Core 5.0 applications but the 6.0.1 web framework was referenced.

    public class MovieControllerTest
    {
        //[Fact]
        public void MainShouldBeForAuthorizedUsersAndReturnView()
            => MyController<MovieController>
                .Instance()
                .Calling(c => c.Main())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();


        /*
        [Theory]
        [InlineData(31414)]
        [InlineData(559)]
        */
        public void MovieCardShouldReturnViewWithValidModel(int id)
            => MyController<MovieController>
                .Instance(controller => controller.WithoutModelState())
                .Calling(c => c.MovieCard(id))
                .ShouldHave()
                .ValidModelState()
                //.Data(data => data.WithSet<Movie>(s => s
                 //   .Any(m => m.MovieId == id))
                //.TempData(tempData => tempData.ContainingEntryWithKey())
                .AndAlso()
                .ShouldReturn()
                .View();

        /*
        [Theory]
        [InlineData(1104)]
        [InlineData(63247)]
        */
        public void ShowCardShouldReturnViewWithValidModel(int id)
            => MyController<MovieController>
                .Instance(controller => controller.WithoutModelState())
                .Calling(c => c.MovieCard(id))
                .ShouldHave()
                .ValidModelState()
                //.Data(data => data.WithSet<Movie>(s => s
                //   .Any(m => m.MovieId == id))
                //.TempData(tempData => tempData.ContainingEntryWithKey())
                .AndAlso()
                .ShouldReturn()
                .View();
    }
}
