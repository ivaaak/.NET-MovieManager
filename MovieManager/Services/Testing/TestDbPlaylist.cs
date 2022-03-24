using Microsoft.EntityFrameworkCore;
using MovieManager.Data;
using MovieManager.Data.DataModels;

namespace MovieManager.Services
{
    public class TestDbPlaylist
    {
        private readonly MovieContext data;

        public TestDbPlaylist(MovieContext data)
        {
            this.data = data; //access db without new contexts
        }


        public static void FillPlaylist()
        {
            //Debug methods
            var collection = new List<int>
            {
                44217, 69740, 414906, 171274, 335984, 5511, 1592
            };
            foreach (var item in collection)
            {
                AddMovieToPlaylist(item, "current", "ivo@ivo.com"); 
            }


            var collection2 = new List<int>
            {
                550,657,60622,399057,398181,
            };
            foreach (var item in collection2)
            {
                AddMovieToPlaylist(item, "watched", "ivo@ivo.com");
            }
        }


        public static void AddMovieToPlaylist(int movieId, string PlaylistName, string UserName)
        {
            var data = new MovieContext();
            var m = data.Movies.Where(m => m.MovieId == movieId).FirstOrDefault();
            if(m != null)
            {
                Console.WriteLine($"Movie {m.Title} found in db !!!");
            }


            var playlist = data.Playlists
                .Include(a => a.Movies)         //needed
                .Where(p => p.User.UserName == UserName && p.PlaylistName == PlaylistName)
                .FirstOrDefault();
            
            if(playlist != null)
            {
                Console.WriteLine($"Playlist {playlist.PlaylistName} found");
            }

            playlist.Movies.Add(m);
            data.Playlists.Update(playlist);
            data.SaveChanges();

            var check = data.Playlists.Where(p => p.PlaylistName == PlaylistName && p.User.UserName == UserName).FirstOrDefault();
            Console.WriteLine($"Movie count in playlist: {check.Movies.Count}");
        }



        public void CreatePlaylist(string playlistName, string userId)
        {
            //IdentityUser user = data.Users.Where(u => u.Id == userId).FirstOrDefault();
            //get user id as this.User.Id so it only works when logged.

            Playlist playlist = new Playlist
            {
                PlaylistName = playlistName,
                //UserId = userId,
                Movies = new List<Movie>(),
                QrCode = "qr-code-bytes here",
            };

            //var userPlaylist = data.UserPlaylists.Where(u => u.UserId == userId).FirstOrDefault();
            //userPlaylist.Playlists.Add(playlist);
            //Console.WriteLine($"Number of playlists for the user: {userPlaylist.Playlists.Count}");
            
            //data.UserPlaylists.Update(userPlaylist);
            data.SaveChanges();
        }

        public void PrintAllPlaylistMovies()
        {
            MovieContext context = new MovieContext();


            foreach (var item in context.Playlists)
            {
                Console.WriteLine($"Playlist - {item.PlaylistName}");
                foreach (var movie in item.Movies)
                {
                    Console.WriteLine($"Movie Name - {movie.Title}");
                }
            }
        }
    }
}
