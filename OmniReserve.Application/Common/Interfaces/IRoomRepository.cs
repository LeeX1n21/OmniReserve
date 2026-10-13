using OmniReserve.Domain.Entities;

namespace OmniReserve.Application.Common.Interfaces;

public interface IRoomRepository
{
    Task AddAsync(Room room);
    Task<Room?> GetByIdAsync(Guid id);
}