using System.Linq;
using MoviesApp.Models;
using AutoMapper;
using MoviesApp.Services.Dto;

namespace MoviesApp.ViewModels.AutoMapperProfiles
{
    public class MovieProfile: Profile
    {
        public MovieProfile()
        {
          CreateMap<MovieDto, InputMovieViewModel>().ReverseMap(); 
          CreateMap<MovieDto, DeleteMovieViewModel>().ReverseMap();
          CreateMap<MovieDto, EditMovieViewModel>().ReverseMap();
          CreateMap<MovieDto, MovieViewModel>()
              .ForMember(dto => dto.ActorsMovie, opt => opt.MapFrom(src => src.Actors
                  .Select(a => new InputActorViewModel()
                  {
                      Id= a.Id,
                      FirstName = a.FirstName,
                      LastName = a.LastName
                  }).ToList()))
              .ReverseMap();
        }
    }
}