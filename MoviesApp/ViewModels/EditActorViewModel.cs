using System.Collections;
using System.Collections.Generic;
using MoviesApp.Models;

namespace MoviesApp.ViewModels
{
    public class EditActorViewModel:InputActorViewModel
    {
        public ICollection<Movie> AllMovies { get; set; }
    }
}