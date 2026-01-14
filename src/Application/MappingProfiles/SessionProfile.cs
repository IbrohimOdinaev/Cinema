using Cinema.Domain.Entities;
using Cinema.Application.DTOS;
using AutoMapper;

namespace Cinema.Application.MappingProfiles;

public class SessionProfile : Profile
{
    public SessionProfile()
    {
        CreateMap<Session, SessionResponse>()
          .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.Duration.Start))
          .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.Duration.End))
          .ForMember(dest => dest.FilmTitle, opt => opt.MapFrom(src => src.Film!.Title
                      ));
    }
}
