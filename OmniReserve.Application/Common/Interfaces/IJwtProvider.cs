using OmniReserve.Domain.Entities;

namespace OmniReserve.Application;

public interface IJwtProvider
{
    string Generate(User user);

}
