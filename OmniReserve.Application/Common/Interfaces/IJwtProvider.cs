using OmniReserve.Domain.Entities;

namespace OmniReserve.Application.Common.Interfaces;

public interface IJwtProvider
{
    string Generate(User user);
}