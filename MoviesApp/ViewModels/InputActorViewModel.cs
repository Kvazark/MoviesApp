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
            Movies = new HashSet<Movie>();
        }
        [Filters.MinLengthAttribute]
        public string FirstName { get; set; }
        
        [Filters.MinLengthAttribute]
        public string LastName { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
        public ICollection<Movie> Movies { get; set; }
    }
}