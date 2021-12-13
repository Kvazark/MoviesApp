using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MoviesApp.Models;
using MoviesApp.Filters;

namespace MoviesApp.ViewModels
{
    public class InputActorViewModel
    {
        public InputActorViewModel()
        {
            ActorMovies = new HashSet<ActorsMovies>();
        }
        public int? Id { get; set; }
        
        [Required]
        [Filters.MinLengthAttribute]
        public string FirstName { get; set; }
        
        [Required]
        [Filters.MinLengthAttribute]
        public string LastName { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
        public ICollection<ActorsMovies> ActorMovies { get; set; }
       
    }
}