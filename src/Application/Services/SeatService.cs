using Cinema.Domain.Entities;
using Cinema.Application.DTOS;
using Cinema.Application.Services.IServices;
using AutoMapper;
using Cinema.Application.Abstractions.IRepositories;
using System.Runtime.CompilerServices;

namespace Cinema.Application.Services;

public class SeatService : ISeatService
{
    private readonly ISeatRepository _seatRepository;
    private readonly IMapper _mapper;

    public SeatService(ISeatRepository seatRepository, IMapper mapper)
    {
        _seatRepository = seatRepository;
        _mapper = mapper;
    }

    public async IAsyncEnumerable<SeatResponse> GetHallSeatsAsync(Guid id, [EnumeratorCancellation] CancellationToken token)
    {
        await foreach (Seat seat in _seatRepository.GetByHallIdAsync(id, token))
        {
            yield return _mapper.Map<SeatResponse>(seat);
        }
    }
}
