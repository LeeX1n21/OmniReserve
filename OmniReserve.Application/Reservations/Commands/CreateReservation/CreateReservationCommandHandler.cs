using MediatR;
using OmniReserve.Application.Common.Interfaces;
using OmniReserve.Domain.Entities;

namespace OmniReserve.Application.Reservations.Commands.CreateReservation;

public record CreateReservationCommand(Guid RoomId, Guid UserId, DateTime StartDate, DateTime EndDate) : IRequest<Guid>;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Guid>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRoomRepository _roomRepository;

    public CreateReservationCommandHandler(
        IReservationRepository reservationRepository,
        IRoomRepository roomRepository)
    {
        _reservationRepository = reservationRepository;
        _roomRepository = roomRepository;
    }

    public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        bool isAvailable = await _reservationRepository.IsRoomAvailableAsync(
            request.RoomId, 
            request.StartDate, 
            request.EndDate);

        if (!isAvailable)
        {
            throw new Exception("La habitación seleccionada ya está ocupada en el rango de fechas solicitado.");
        }

        var reservation = new Reservation
        {
            Id = Guid.NewGuid(),
            RoomId = request.RoomId,
            UserId = request.UserId,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };

        await _reservationRepository.AddAsync(reservation);

        return reservation.Id;
    }
}