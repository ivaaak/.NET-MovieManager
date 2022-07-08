namespace MovieManager.Api.ApiServices.Models
{
    public class PlaylistMovie
    {
        public string PlaylistId { get; set; }
        public Playlist Playlist { get; set; }


        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
