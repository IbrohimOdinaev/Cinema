using Cinema.Application.DTOS;
using Cinema.Application.Abstractions.IRepositories;
using Cinema.Application.Services.IServices;
using Cinema.Domain.Entities;
using AutoMapper;
using System.Runtime.CompilerServices;

namespace Cinema.Application.Services;

public class HallService : IHallService
{
    private readonly IHallRepository _hallRepository;
    private readonly IMapper _mapper;

    public HallService(IHallRepository hallRepository, IMapper mapper)
    {
        _hallRepository = hallRepository;
        _mapper = mapper;
    }

    public async IAsyncEnumerable<HallResponse> GetAllAsync([EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var hall in _hallRepository.GetAllAsync(token))
        {
            yield return _mapper.Map<HallResponse>(hall);
        }
    }

    public async Task<HallResponse?> CreateAsync(CreateHallRequest hallDto, CancellationToken token)
    {
        var hall = _mapper.Map<Hall>(hallDto);

        try
        {
            await _hallRepository.CreateAsync(hall, token);

            return _mapper.Map<HallResponse>(hall);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        try
        {
            bool result = await _hallRepository.DeleteAsync(id, token);

            return result;
        }
        catch (Exception)
        {
            return false;
        }
    }



}
