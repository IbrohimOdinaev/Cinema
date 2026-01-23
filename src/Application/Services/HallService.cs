using Cinema.Application.DTOS;
using Cinema.Application.Abstractions.IRepositories;
using Cinema.Application.Services.IServices;
using Cinema.Domain.Entities;
using AutoMapper;
using System.Runtime.CompilerServices;
using FluentResults;
using Cinema.Application.Exceptions;

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

    public async Task<Result<HallResponse>> CreateAsync(CreateHallRequest hallDto, CancellationToken token)
    {
        Hall hall = new(hallDto.Title, hallDto.Raw, hallDto.Column);

        try
        {
            await _hallRepository.CreateAsync(hall, token);

            return Result.Ok(_mapper.Map<HallResponse>(hall));
        }
        catch (ApplicationPersistenceException ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken token)
    {
        try
        {
            bool result = await _hallRepository.DeleteAsync(id, token);

            return result ? Result.Ok() : Result.Fail("Hall not found");
        }
        catch (ApplicationPersistenceException ex)
        {
            return Result.Fail(ex.Message);
        }
    }



}
