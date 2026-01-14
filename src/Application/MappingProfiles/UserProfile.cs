using AutoMapper;
using Cinema.Domain.Entities;
using Cinema.Application.DTOS;
using Cinema.Domain.ValueObjects;

namespace Cinema.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserRequest, User>()
            .ConstructUsing(src =>
                new User(
                    src.Name,
                    Email.Create(src.Email),
                    src.Password,
                    src.Role
                    ));

        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value));
    }
}
