using Cinema.Application.DTOS;
using Cinema.Domain.Entities;
using Cinema.Application.Abstractions.IRepositories;
using System.Runtime.CompilerServices;
using AutoMapper;
using Cinema.Application.Abstractions;
using Cinema.Application.Services.IServices;

namespace Cinema.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly ISessionRepository _sessionRepository;
    private readonly IUserRepository _userRepository;
    private readonly ISeatRepository _seatRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;


    public BookingService(IBookingRepository bookingRepository, IMapper mapper, ISessionRepository sessionRepository, IUserRepository userRepository, IUnitOfWork uow, ISeatRepository seatRepository)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
        _sessionRepository = sessionRepository;
        _userRepository = userRepository;
        _uow = uow;
        _seatRepository = seatRepository;
    }

    public async Task<BookingResponse?> CreateAsync(CreateBookingRequest bookingDto, CancellationToken token)
    {
        var session = await _sessionRepository.GetByIdWithNavigationPropertyAsync(bookingDto.SessionId, token);
        var user = await _userRepository.GetByIdAsync(bookingDto.UserId, token);

        if (session is null || user is null) return null;

        decimal bookingCost = session.Film!.Price * bookingDto.Seats.Count();

        if (user.Wallet.Balance < bookingCost) return null;

        Booking booking = new(bookingDto.UserId, bookingDto.SessionId, bookingCost, bookingDto.Seats);
        await _uow.BeginAsync(token);
        try
        {
            user.Wallet.Deduct(booking.Cost);
            var r1 = await _userRepository.UpdateAsync(user);
            var r2 = await _bookingRepository.CreateAsync(booking, token);

            if (r2 is null || r1 is null) throw new Exception();

            List<RawNum> positions = new();

            foreach (var seatId in booking.Seats)
            {
                var seat = await _seatRepository.GetByIdAsync(seatId, token);

                seat!.ChangeStatus(true);
                await _seatRepository.UpdateAsync(seat);
                positions.Add(new RawNum(seat.Position.Raw, seat.Position.Num));
            }
            await _uow.CommitAsync();
            return new BookingResponse(booking.Id, session.Id, session.Film.Title, booking.Cost, positions);
        }
        catch (Exception)
        {
            await _uow.RollbackAsync(token);
            return null;
        }
    }

}
