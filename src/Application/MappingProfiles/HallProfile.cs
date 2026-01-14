using Cinema.Domain.Entities;
using Cinema.Application.DTOS;
using AutoMapper;

public class HallProfile : Profile
{
    public HallProfile()
    {
        CreateMap<Hall, HallResponse>();
    }
}
