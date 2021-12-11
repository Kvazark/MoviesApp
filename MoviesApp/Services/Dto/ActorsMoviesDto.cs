using MoviesApp.Models;
using System.Collections.Generic;

namespace MoviesApp.Services.Dto
{
    public class ActorsMoviesDto
    {
        public virtual ICollection<ActorsMovies> ActorsMovies { get; set; }
    }
}