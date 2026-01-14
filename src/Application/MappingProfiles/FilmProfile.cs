using Cinema.Domain.Entities;
using Cinema.Application.DTOS;
using AutoMapper;

namespace Cinema.Application.MappingProfiles;

public class FilmProfile : Profile
{
    public FilmProfile()
    {
        CreateMap<CreateFilmRequest, Film>()
          .ConstructUsing(src =>
            new Film(
              src.Title,
              src.Price,
              src.Duration
            ));

        CreateMap<Film, FilmResponse>();
    }
}
