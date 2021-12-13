using System.Linq;
using AutoMapper;
using MoviesApp.Models;
using MoviesApp.ViewModels;

namespace MoviesApp.Services.Dto.AutoMapperProfiles
{
    public class MovieDtoProfile:Profile
    {
        public MovieDtoProfile()
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(dto => dto.Actors, opt => opt.MapFrom(src => src.ActorsMovies
                    .Select(a => new ActorDto
                    {
                        Id = a.ActorId,
                        FirstName = a.Actor.FirstName,
                        LastName = a.Actor.LastName,
                    }).ToList()))
                .ReverseMap();
        }
    }
}