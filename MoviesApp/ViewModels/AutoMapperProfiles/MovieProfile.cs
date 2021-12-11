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
          CreateMap<MovieDto, MovieViewModel>();
        }
    }
}