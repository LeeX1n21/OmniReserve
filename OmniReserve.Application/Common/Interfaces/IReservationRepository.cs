using OmniReserve.Domain.Entities;

namespace OmniReserve.Application.Common.Interfaces;

public interface IReservationRepository
{
    Task<Reservation?> GetReservationWithDetailsAsync(Guid id);
    Task<bool> IsRoomAvailableAsync(Guid roomId, DateTime start, DateTime end);
}