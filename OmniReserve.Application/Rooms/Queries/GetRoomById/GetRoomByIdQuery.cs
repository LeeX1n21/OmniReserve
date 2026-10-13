using MediatR;

namespace OmniReserve.Application.Rooms.Queries.GetRoomById;

public class GetRoomByIdQuery : IRequest<RoomResponseDto>
{
    public Guid RoomId { get; set; }
}