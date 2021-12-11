using MoviesApp.Models;
using AutoMapper;
using MoviesApp.Services.Dto;
using System.Linq;

namespace MoviesApp.ViewModels.AutoMapperProfiles
{
    public class ActorProfile: Profile
    {
        public ActorProfile()
        {
            CreateMap<ActorDto, InputActorViewModel>().ReverseMap();
            CreateMap<ActorDto, DeleteActorViewModel>().ReverseMap();
            CreateMap<ActorDto, EditActorViewModel>().ReverseMap();
            CreateMap<ActorDto, ActorViewModel>()
                .ForMember(dto => dto.ActorMovies, opt => opt.MapFrom(src => src.Movies
                    .Select(a => new InputMovieViewModel()
                    {
                        Id= a.Id,
                        Title = a.Title
                    }).ToList()))
                .ReverseMap();
        }
    }
}