using System.Collections.Generic;
using MoviesApp.Models;

namespace MoviesApp.ViewModels
{
    public class ActorViewModel:InputActorViewModel
    {
        public int Id { get; set; }
        public virtual ICollection<InputMovieViewModel> ActorMovies { get; set; }
    }
}