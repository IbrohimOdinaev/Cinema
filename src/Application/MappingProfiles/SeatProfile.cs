using Cinema.Domain.Entities;
using Cinema.Application.DTOS;
using AutoMapper;

namespace Cinema.Application.MappingProfiles;

public class SeatProfile : Profile
{
    public SeatProfile()
    {
        CreateMap<Seat, SeatResponse>()
          .ForMember(dest => dest.Num, opt => opt.MapFrom(src => src.Position.Num))
          .ForMember(dest => dest.Num, opt => opt.MapFrom(src => src.Position.Raw));
    }
}
