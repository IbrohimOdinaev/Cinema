using Microsoft.AspNetCore.Mvc;
using Cinema.Application.Services.IServices;
using System.Runtime.CompilerServices;
using Cinema.Application.DTOS;

namespace Cinema.Api.Controllers;

[ApiController]
[Route("/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest userDto, CancellationToken token)
    {
        var result = await _userService.CreateAsync(userDto, token);

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AddMoney([FromRoute] Guid id, CancellationToken token)
    {
        var result = await _userService.AddMoney(id, token);

        return Ok(result);
    }

    [HttpGet]
    public IAsyncEnumerable<UserResponse> GetAll(CancellationToken token)
    {
        return _userService.GetAllAsync(token);
    }
}
