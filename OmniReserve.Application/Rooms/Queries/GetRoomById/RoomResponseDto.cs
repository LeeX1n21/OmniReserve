namespace OmniReserve.Application.Rooms.Queries.GetRoomById;

public record RoomResponseDto(Guid Id, string RoomNumber, string Type, decimal Price, bool IsAvailable);