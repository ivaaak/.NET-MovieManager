using TMDbLib.Objects.TvShows;

namespace MovieManager.Models
{
    public class ShowCardViewModel
    {
        public ShowCardViewModel() { }

        public TvShow Show { get; set; }

        public List<Cast> ShowActorsList { get; set; }
    }
}
