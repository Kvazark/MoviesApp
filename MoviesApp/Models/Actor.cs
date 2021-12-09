using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Models
{
    public class Actor
    {
        public Actor()
        {
            Movies = new HashSet<ActorsMovies>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
       public virtual ICollection<ActorsMovies> Movies { get; set; }
    }
}