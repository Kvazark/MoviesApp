using AutoMapper;
using MoviesApp.Models;
using System.Linq;
using MoviesApp.ViewModels;

namespace MoviesApp.Services.Dto.AutoMapperProfiles
{
    public class ActorDtoProfile:Profile
    {
        public ActorDtoProfile()
        {
            CreateMap<Actor, ActorDto>()
                .ForMember(dto => dto.Movies, opt => opt.MapFrom(src => src.ActorsMovies
                    .Select(m => new MovieDto
                    {
                        Id = m.MovieId,
                        Title = m.Movie.Title,
                    }).ToList()))
                .ReverseMap();
                
        }
    }
}