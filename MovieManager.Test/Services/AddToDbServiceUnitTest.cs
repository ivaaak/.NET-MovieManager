using Autofac.Extras.Moq;
using MovieManager.Data.DataModels;
using MovieManager.Services;
using Xunit;

namespace MovieManager.Test
{
    public class AddToDbServiceUnitTest
    {
        private Playlist playlist;
        private Movie movie;

        public void Setup()
        {
            this.playlist = new Playlist()
            {

            };
            this.movie = new Movie()
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
        }


        [Fact]
        //[InlineData()]
        public void AddMovieToUserPlaylist_ValidCall(int movieId, string PlaylistName, string Name)
        {
            using (var playlistMock = AutoMock.GetLoose()) //GetStrict?
            {
                //playlistMock.Mock<MovieContext>().Setup(x => x.Playlists());
                //playlistMock.MockRepository

            }

            AddToDbService service = new AddToDbService();
            service.AddMovieToUserPlaylist(movieId, PlaylistName, Name);    
            //Assert.AreEqual();
        }
    }
}