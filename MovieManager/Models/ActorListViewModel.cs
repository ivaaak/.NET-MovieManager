using MovieManager.Data.DataModels;
using System.ComponentModel.DataAnnotations;

namespace MovieManager.Models
{
    public class ActorListViewModel
    {
        [Required]
        public List<Actor> Actors { get; set; }
    }
}