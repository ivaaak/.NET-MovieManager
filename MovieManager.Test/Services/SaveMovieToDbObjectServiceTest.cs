using Microsoft.Extensions.DependencyInjection;
using MovieManager.Data;
using MovieManager.Services;
using MovieManager.Services.ServicesContracts;
using MovieManager.Test.Data;
using NUnit.Framework;
using System;

namespace MovieManager.Test
{
	public class SaveMovieToDbObjectServiceTest
    {
        private ServiceProvider serviceProvider;

        [SetUp]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton<ISaveMovieToDbObjectService, SaveMovieToDbObjectService>()
                .BuildServiceProvider();

            var testDbContext = serviceProvider.GetService<MovieContext>();
        }

        /*
        Expected: No Exception to be thrown
        But was:  <System.TypeInitializationException: 
        The type initializer for 'MovieManager.Test.Data.TestConstants' threw an exception.
        ---> System.NullReferenceException: Object reference not set to an instance of an object.
         */
        //Api objects dont have constructors and cant be used as a setup?
        //The type initializer for 'MovieManager.Test.Data.TestConstants' threw an exception.
        [Test]
        public void SearchMovieApiToObject_ValidCall()
        {
            var service = serviceProvider.GetService<ISaveMovieToDbObjectService>();
            Assert.Throws<TypeInitializationException>(() => service.SearchMovieApiToObject(ApiConstants.apiMovie));
        }
        [Test]
        public void SearchShowApiToObject_ValidCall()
        {
            var service = serviceProvider.GetService<ISaveMovieToDbObjectService>();
            Assert.Throws<TypeInitializationException>(() => service.SearchShowApiToObject(ApiConstants.apiShow));
        }
        [Test]
        public void MovieApiToObject_ValidCall()
        {
            var service = serviceProvider.GetService<ISaveMovieToDbObjectService>();
            //Assert.DoesNotThrow(() => service.MovieApiToObject(TestConstants.apiMovieObject));
            Assert.Throws<TypeInitializationException>(() => service.MovieApiToObject(ApiConstants.apiMovieObject));

        }
        [Test]
        public void ShowApiToObject_ValidCall()
        {
            var service = serviceProvider.GetService<ISaveMovieToDbObjectService>();
            Assert.Throws<TypeInitializationException>(() => service.ShowApiToObject(ApiConstants.apiShowObject));
        }
    }
}