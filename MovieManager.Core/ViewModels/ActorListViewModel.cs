using MovieManager.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieManager.Core.ViewModels
{
    public class ActorListViewModel
    {
        [Required]
        public List<Actor> Actors { get; set; }
    }
}