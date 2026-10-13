using OmniReserve.Domain.Enums;

namespace OmniReserve.Domain.Entities;

public class Reservation
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid RoomId { get; private set; }
    public DateTime CheckInDate { get; private set; }
    public DateTime CheckOutDate { get; private set; }
    public decimal TotalPrice { get; private set; }
    public ReservationStatus Status { get; private set; }

    public Reservation(Guid userId, Guid roomId, DateTime checkInDate, DateTime checkOutDate, decimal totalPrice)
    {
        if (checkInDate >= checkOutDate)
            throw new ArgumentException("La fecha de check-in debe ser anterior a la de check-out.");

        Id = Guid.NewGuid();
        UserId = userId;
        RoomId = roomId;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        TotalPrice = totalPrice;
        Status = ReservationStatus.Pending;
    }

}