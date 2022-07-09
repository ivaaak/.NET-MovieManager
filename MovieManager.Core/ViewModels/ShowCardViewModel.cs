using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.TvShows;

namespace MovieManager.Core.ViewModels
{
    public class ShowCardViewModel
    {
        public ShowCardViewModel() { }

        public TvShow Show { get; set; }

        public List<Cast> ShowActorsList { get; set; }

        public List<ReviewBase> Reviews { get; set; }
    }
}
