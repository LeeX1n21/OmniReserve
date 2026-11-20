using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Security.Claims;
using OmniReserve.Domain.Constants;
using OmniReserve.Application.Reservations.Commands.CreateReservation;
using OmniReserve.Application.Reservations.Queries.GetAllReservations;

namespace OmniReserve.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReservationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReservationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("all")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> GetAllReservations()
    {
        var query = new GetAllReservationsQuery();
        var reservations = await _mediator.Send(query);

        return Ok(reservations);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationRequest request)
    {
        var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized("Token inválido: No contiene información de usuario.");
        }

        var userId = Guid.Parse(userIdClaim);

        var command = new CreateReservationCommand(
            RoomId: request.RoomId,
            UserId: userId,
            StartDate: request.StartDate,
            EndDate: request.EndDate);

        var reservationId = await _mediator.Send(command);

        return Ok(reservationId);
    }
}