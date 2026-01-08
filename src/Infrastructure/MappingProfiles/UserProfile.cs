using Cinema.Domain.Entities;
using Cinema.Domain.ValueObjects;
using Cinema.Infrastructure.DbEntities;
using AutoMapper;

namespace Cinema.Infrastructure.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, DbUser>()
          .ForMember(dest => dest.WalletBalance,
              opt => opt.MapFrom(src => src.Wallet.Balance));

        CreateMap<DbUser, User>()
          .ConstructUsing((src, context) => new User
          {
              Id = src.Id,
              Name = src.Name,
              PasswordHash = src.PasswordHash,
              Role = src.Role,
              Wallet = new Wallet(src.WalletBalance),
              Bookings = context.Mapper.Map<List<Booking>>(src.Bookings)
          });
    }
}
