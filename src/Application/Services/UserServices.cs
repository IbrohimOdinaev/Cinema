using Cinema.Application.DTOS;
using Cinema.Domain.Entities;
using Cinema.Application.Abstractions.IRepositories;
using Cinema.Application.Abstractions;
using Cinema.Application.Services.IServices;
using AutoMapper;
using System.Runtime.CompilerServices;

namespace Cinema.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _hasher;

    public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher hasher)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _hasher = hasher;
    }

    public async IAsyncEnumerable<UserResponse> GetAllAsync([EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var user in _userRepository.GetAllAsync(token))
        {
            yield return _mapper.Map<UserResponse>(user);
        }
    }

    public async Task<UserResponse?> CreateAsync(CreateUserRequest userDto, CancellationToken token)
    {
        var user = _mapper.Map<User>(userDto with { Password = _hasher.Hash(userDto.Password) });

        try
        {
            await _userRepository.CreateAsync(user, token);

            return _mapper.Map<UserResponse>(user);
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
            bool result = await _userRepository.DeleteAsync(id, token);

            return result;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<UserResponse?> AddMoney(Guid id, CancellationToken token)
    {
        var user = await _userRepository.GetByIdAsync(id, token);

        if (user is null) return null;

        user.Wallet.Add(1000);

        await _userRepository.UpdateAsync(user, token);

        return _mapper.Map<UserResponse>(user);
    }
}
