using Microsoft.Extensions.DependencyInjection;
using OmniReserve.Application.Common.Interfaces;
using OmniReserve.Infrastructure.Persistence.Repositories;

namespace OmniReserve.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IRoomRepository, RoomRepository>();
        return services;
    }
}