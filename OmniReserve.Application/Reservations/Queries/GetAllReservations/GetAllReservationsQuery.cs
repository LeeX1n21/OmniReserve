using MediatR;
using OmniReserve.Domain.Entities;

namespace OmniReserve.Application.Reservations.Queries.GetAllReservations;

public record GetAllReservationsQuery() : IRequest<List<Reservation>>;