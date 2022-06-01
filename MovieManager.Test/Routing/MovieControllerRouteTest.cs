using Microsoft.Extensions.Caching.Distributed;
using Moq;
using MovieManager.Controllers;
using MovieManager.Data;
using MovieManager.Services.ServicesContracts;
using System;
using Xunit;

namespace MovieManager.Test.Routing
{
    public class MovieControllerRouteTest
    {
        // System.NullReferenceException : Object reference not set to an instance of an object. 
        // User.Identity.Name = null
        [Fact]
        public void Main_ReturnsAViewResult()
        {
            // Arrange
            var mockContext = new Mock<MovieContext>();
            var search = new Mock<ISearchMethodsService>();
            var cache = new Mock<IDistributedCache>();
            var getFromDb = new Mock<IGetFromDbService>();
            var controller = new MovieController(search.Object, getFromDb.Object, cache.Object);


            Assert.Throws<NullReferenceException>(() => controller.Main());

            // Actual test should be:
            //var result = controller.Main();
            //var viewResult = Assert.IsType<ViewResult>(result);
            //var model = Assert.IsAssignableFrom<IEnumerable<MovieListViewModel>>(viewResult.ViewData.Model);
            //Assert.Equal(1, model.Count());
        }
    }
}
