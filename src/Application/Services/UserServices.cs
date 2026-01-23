using Cinema.Application.DTOS;
using Cinema.Domain.Entities;
using Cinema.Application.Abstractions.IRepositories;
using Cinema.Application.Abstractions;
using Cinema.Application.Services.IServices;
using AutoMapper;
using System.Runtime.CompilerServices;
using FluentResults;
using Cinema.Application.Exceptions;
using Cinema.Domain.Enums;
using Cinema.Domain.ValueObjects;

namespace Cinema.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtTokenGenerator _jwt;

    public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher hasher, IJwtTokenGenerator jwt)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _hasher = hasher;
        _jwt = jwt;
    }

    public async Task<Result> CreateAdminAsync(CancellationToken token)
    {
        User admin = new("admin", Email.Create("admin@gmail.com"), _hasher.Hash("admin"), Role.Admin);

        try
        {
            var result = await _userRepository.CreateAsync(admin);

            return result is not null ? Result.Ok() : Result.Fail("Admin already exists");
        }
        catch (ApplicationPersistenceException ex)
        {
            return Result.Fail(ex.Message);
        }
    }
    public async IAsyncEnumerable<UserResponse> GetAllAsync([EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var user in _userRepository.GetAllAsync(token))
        {
            yield return _mapper.Map<UserResponse>(user);
        }
    }

    public async Task<Result<string>> LoginAsync(LoginRequest loginDto, CancellationToken token)
    {
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);

        if (user is null) return Result.Fail("User not found");

        if (_hasher.Verify(loginDto.Password, user.PasswordHash))
        {
            return Result.Ok(await _jwt.GenerateToken(user.Id, user.Name, user.Email.Value, user.Role));
        }
        else
        {
            return Result.Fail("UnCorrect password");
        }
    }
    public async Task<Result<UserResponse>> CreateAsync(CreateUserRequest userDto, CancellationToken token)
    {
        var user = _mapper.Map<User>(userDto with { Password = _hasher.Hash(userDto.Password) });

        if (await _userRepository.GetByEmailAsync(userDto.Email) is not null) return Result.Fail("User with this email already exists");
        try
        {
            await _userRepository.CreateAsync(user, token);

            return _mapper.Map<UserResponse>(user);
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
            bool result = await _userRepository.DeleteAsync(id, token);

            return result == true ? Result.Ok() : Result.Fail("User not found");
        }
        catch (ApplicationPersistenceException ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<UserResponse>> AddMoneyAsync(decimal amount, Guid id, CancellationToken token)
    {
        var user = await _userRepository.GetByIdAsync(id, token);

        if (user is null) return Result.Fail("User not found");

        user.Wallet.Add(amount);
        try
        {
            await _userRepository.UpdateAsync(user, token);

            return Result.Ok(_mapper.Map<UserResponse>(user));
        }
        catch (ApplicationPersistenceException ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<UserResponse>> GetByIdAsync(Guid id, CancellationToken token)
    {
        var user = await _userRepository.GetByIdAsync(id, token);

        return user is null ? Result.Fail("User not found") : Result.Ok(_mapper.Map<UserResponse>(user));
    }
}
