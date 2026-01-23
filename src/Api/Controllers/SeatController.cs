using Microsoft.AspNetCore.Mvc;
using Cinema.Application.Services.IServices;
using Cinema.Application.DTOS;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.Api.Controlleres;

[ApiController]
[Route("/seat")]
public class SeatController : ControllerBase
{
    private readonly ISeatService _seatService;

    public SeatController(ISeatService seatService)
    {
        _seatService = seatService;
    }
}

