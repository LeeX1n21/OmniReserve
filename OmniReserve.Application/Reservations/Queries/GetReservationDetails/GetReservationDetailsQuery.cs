using MediatR;

namespace OmniReserve.Application.Reservations.Queries.GetReservationDetails;

public record GetReservationDetailsQuery(Guid ReservationId) : IRequest<ReservationDetailsDto>;