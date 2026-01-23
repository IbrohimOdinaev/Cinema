using Cinema.Application.DTOS;
using Cinema.Domain.Entities;
using Cinema.Application.Abstractions.IRepositories;
using System.Runtime.CompilerServices;
using AutoMapper;
using Cinema.Application.Abstractions;
using Cinema.Application.Services.IServices;
using Cinema.Application.Exceptions;
using FluentResults;

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

    public async Task<Result<BookingResponse>> CreateAsync(CreateBookingRequest bookingDto, Guid userId, CancellationToken token)
    {
        var session = await _sessionRepository.GetByIdWithNavigationPropertyAsync(bookingDto.SessionId, token);
        var user = await _userRepository.GetByIdAsync(userId, token);
        if (session is null || user is null)
            return session is null ? Result.Fail("Session not found") : Result.Fail("User not found");

        decimal bookingCost = session.Film!.Price * bookingDto.Seats.Count();
        if (user.Wallet.Balance < bookingCost) return Result.Fail("Not enough money");

        Booking booking = new(userId, bookingDto.SessionId, bookingCost, bookingDto.Seats);

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

                if (seat is null || seat.IsOccupied is true)
                    throw new Exception();

                seat!.ChangeStatus(true);
                await _seatRepository.UpdateAsync(seat);
                positions.Add(new RawNum(seat.Position.Raw, seat.Position.Num));
            }

            await _uow.CommitAsync();

            return new BookingResponse(booking.Id, session.Id, session.Film.Title, booking.Cost, positions);
        }
        catch
        {
            await _uow.RollbackAsync(token);
            return Result.Fail("Transaction Failed");
        }
    }

    public async IAsyncEnumerable<BookingResponse> GetUserBookingsAsync(Guid id, [EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var booking in _bookingRepository.GetByUserIdAsync(id, token))
        {
            yield return _mapper.Map<BookingResponse>(booking);
        }
    }
}
