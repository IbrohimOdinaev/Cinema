using AutoMapper;

namespace Cinema.Infrastructure.MappingProfiles;

public class BaseProfile<T1, T2> : Profile
{
    public BaseProfile()
    {
        CreateMap<T1, T2>().ReverseMap();
    }
}
