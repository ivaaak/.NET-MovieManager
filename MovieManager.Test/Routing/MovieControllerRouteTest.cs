using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MovieManager.Controllers;
using MovieManager.Data;
using MovieManager.Models;
using MovieManager.Services.ServicesContracts;
using System.Collections.Generic;
using System.Linq;
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
            var logger = new Mock<ILogger<MovieController>>();
            var search = new Mock<ISearchMethodsService>();
            var apiGet = new Mock<IApiGetListsService>();
            var getFromDb = new Mock<IGetFromDbService>();

            var controller = new MovieController(logger.Object, search.Object, apiGet.Object, getFromDb.Object);

            // Act
            var result = controller.Main();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<MovieListViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}
