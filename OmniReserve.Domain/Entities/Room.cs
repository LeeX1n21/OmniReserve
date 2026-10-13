using OmniReserve.Domain.Enums;

namespace OmniReserve.Domain.Entities;

public class Room
{
    public Guid Id { get; private set; }
    public string RoomNumber { get; private set; }
    public RoomType Type { get; private set; }
    public decimal PricePerNight { get; private set; }
    public bool IsAvailable { get; private set; }

    public Room(string roomNumber, RoomType type, decimal pricePerNight)
    {
        Id = Guid.NewGuid();
        RoomNumber = roomNumber;
        Type = type;
        PricePerNight = pricePerNight;
        IsAvailable = true;
    }

    public void MarkAsUnavailable()
    {
        IsAvailable = false;
    }

    public void MarkAsAvailable()
    {
        IsAvailable = true;
    }

}