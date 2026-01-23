using Microsoft.AspNetCore.Mvc;
using Cinema.Application.Services.IServices;
using Cinema.Application.DTOS;
using Microsoft.AspNetCore.Authorization;
using static Cinema.Api.Extensions.ClaimExtensions;
using Microsoft.AspNetCore.Authorization;
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

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginDto, CancellationToken token)
    {
        var result = await _userService.LoginAsync(loginDto, token);

        if (result.IsSuccess)
            return Ok(new { accessToken = result.Value });

        return BadRequest(result.Errors.First().Message);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest userDto, CancellationToken token)
    {
        var result = await _userService.CreateAsync(userDto, token);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Errors.First().Message);
    }

    [Authorize]
    [HttpPut("{amount:decimal}")]
    public async Task<IActionResult> AddMoney(decimal amount, CancellationToken token)
    {
        Guid userId = User.GetUserId();

        var result = await _userService.AddMoneyAsync(amount, userId);

        if (result.IsSuccess)
            return Ok();

        return BadRequest(result.Errors.First().Message);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IAsyncEnumerable<UserResponse> GetAll(CancellationToken token)
    {
        return _userService.GetAllAsync(token);
    }

    [AllowAnonymous]
    [HttpPost("/createAdmin")]
    public async Task<IActionResult> CreateAdmin(CancellationToken token)
    {
        var result = await _userService.CreateAdminAsync(token);

        if (result.IsSuccess)
            return Ok("Admin created");

        return BadRequest(result.Errors.First().Message);
    }

    [Authorize(Roles = "User")]
    [HttpDelete]
    public async Task<IActionResult> Delete(CancellationToken token)
    {
        Guid userId = User.GetUserId();

        var result = await _userService.DeleteAsync(userId);

        if (result.IsSuccess)
            return Ok("Account has been deleted");

        return BadRequest(result.Errors.First().Message);
    }
}
