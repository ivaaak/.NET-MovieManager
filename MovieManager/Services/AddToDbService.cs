using Microsoft.EntityFrameworkCore;
using MovieManager.Data;
using MovieManager.Data.DataModels;
using MovieManager.Data.DBConfig;
using MovieManager.Services.ServicesContracts;
using QRCoder;
using System.Text;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Search;

namespace MovieManager.Services
{
    public class AddToDbService : IAddToDbService
    {
        private readonly MovieContext dataContext;

        private ISaveMovieToDbObjectService saveMovieFromApiToDbObject;

        private TMDbClient tmdbClient;

        public AddToDbService(
            ISaveMovieToDbObjectService saveMovieToDbObjectService, 
            MovieContext data) 
        {
            this.saveMovieFromApiToDbObject = saveMovieToDbObjectService;
            this.dataContext = data;
            tmdbClient = new TMDbClient(Configuration.APIKey);
        }


        public void AddMovieToUserPlaylist(int movieId, string PlaylistName, string Name)
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
            var movie = dataContext.Movies.Where(m => m.MovieId == movieId).FirstOrDefault();

            if (movie == null) //movie doesnt exist in db
            {
                var apiMovie = tmdbClient.GetTvShowAsync(movieId).Result;
                movie = saveMovieFromApiToDbObject.ShowApiToObject(apiMovie);  //turn to db object
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

        public void AddMovieToFavorites(int movieId, string Name)
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


        //ACTORS
        public void AddActorToUserList(int ActorId, string Name)
        {
            Actor actor = dataContext.Actors.Where(m => m.ActorId == ActorId).FirstOrDefault();

            if (actor == null) //actor doesnt exist in db
            {
                Person actorApi = new Person();
                MovieCredits credits = new MovieCredits();
                try
                {
                    actorApi = tmdbClient.GetPersonAsync(ActorId).Result;
                    credits = tmdbClient.GetPersonMovieCreditsAsync(ActorId).Result;
                }
                catch (Exception e) { e.ToString(); };

                actor = new Actor()
                {
                    ActorId = actorApi.Id,
                    FullName = actorApi.Name,
                    CountryCode = actorApi.PlaceOfBirth,
                    Overview = actorApi.Biography,
                    //MovieCredits = credits,
                    PhotoUrl = SaveMovieToDbObjectService.BuildImageURL(actorApi.ProfilePath),
                };

                dataContext.Actors.Add(actor);
            };

            var targetUser = dataContext.Users
                .Include(p => p.Actors)
                .Where(u => u.UserName == Name)
                .FirstOrDefault();

            if (!targetUser.Actors.Contains(actor))
            {
                targetUser.Actors.Add(actor);
            }

            dataContext.SaveChanges();

            Console.WriteLine($"Added {actor.FullName} to user - {Name}'s list of saved actors");
        }

        //Reviews
        public void AddReviewToUsersReviews(Review review, string Name)
        {
            var user = dataContext.Users.Where(m => m.UserName == Name).FirstOrDefault();

            Review reviewData = new Review()
            {
                ReviewId = review.ReviewId,
                ReviewTitle = review.ReviewTitle,
                Rating = review.Rating,
                ReviewContent = review.ReviewContent,
                MovieId = review.MovieId,
                UserId = user.Id,
            };
            //user.Reviews.Add(reviewData);

            dataContext.SaveChanges();

            Console.WriteLine($"Added review to user - {Name}'s list of saved actors");
        }

        //QrCode
        public void GenerateQRCode(string playlistId)
        {
            var playlist = dataContext.Playlists
                .Include(p => p.Movies)
                .Where(p => p.PlaylistId == playlistId)
                .FirstOrDefault();

            StringBuilder playlistToText = new StringBuilder();
            foreach (var item in playlist.Movies)
            {
                playlistToText.Append(item.MovieId + "-" + item.Title + ", ");
                Console.WriteLine($"QR - ID - {item.MovieId} QR - Movie - {item.Title}"); //debug
            }

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(playlistToText.ToString(), QRCodeGenerator.ECCLevel.Q);
            Base64QRCode base64Code = new Base64QRCode(qrCodeData);
            var workingStringImage = base64Code.GetGraphic(10);

            QRCodeObject qrCodeObj = new QRCodeObject()
            {
                TextContent = playlistToText.ToString(),
                PlaylistId = playlist.PlaylistId,
                QrCodeImage = workingStringImage,
            };
            if (!dataContext.QRCodes.Any(q => q.PlaylistId == qrCodeObj.PlaylistId)) //skip adding if a qr code exists already
            {
                playlist.QrCode = qrCodeObj;
                dataContext.QRCodes.Add(qrCodeObj);
                dataContext.SaveChanges();
            }
        }
    }
}
