namespace OmniReserve.Application.Reservations.Queries.GetReservationDetails;

public class ReservationDetailsDto
{
    public Guid Id { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public string RoomNumber { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}