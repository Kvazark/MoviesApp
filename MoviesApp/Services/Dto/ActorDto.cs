using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MoviesApp.Filters;

namespace MoviesApp.Services.Dto
{
    public class ActorDto
    {
        public int? Id { get; set; }
        
        [Required]
        [Filters.MinLength]
        public string FirstName { get; set; }
        
        [Required]
        [Filters.MinLength]
        public string LastName { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
        public virtual ICollection<MovieDto> Movies { get; set; }
        
    }
}