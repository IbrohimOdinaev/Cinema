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
    private readonly IBookingService _bookingService;

    public UserController(IUserService userService, IBookingService bookingService)
    {
        _userService = userService;
        _bookingService = bookingService;
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
        var result = await _userService.AddMoneyAsync(id, token);

        return Ok(result);
    }

    [HttpGet]
    public IAsyncEnumerable<UserResponse> GetAll(CancellationToken token)
    {
        return _userService.GetAllAsync(token);
    }
}
