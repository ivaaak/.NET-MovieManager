namespace MovieManager.Data.DataModels
{
    public class PlaylistMovie
    {
        public PlaylistMovie(){}   //many to many

        public string PlaylistId { get; set; }
        public Playlist Playlist { get; set; }


        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
