using OmniReserve.Application.Common.Interfaces;
using OmniReserve.Domain.Entities;

namespace OmniReserve.Infrastructure.Persistence.Repositories;

public class RoomRepository : IRoomRepository
{
    // Diccionario para mantener las habitaciones en memoria viva
    private static readonly Dictionary<Guid, Room> _rooms = new();

    public Task AddAsync(Room room)
    {
        _rooms[room.Id] = room;
        return Task.CompletedTask;
    }

    public Task<Room?> GetByIdAsync(Guid id)
    {
        _rooms.TryGetValue(id, out var room);
        
        return Task.FromResult(room);
    }
}
