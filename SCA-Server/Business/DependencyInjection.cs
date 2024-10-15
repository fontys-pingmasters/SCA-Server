using Business.Implementations;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IMatchService, MatchService>();
        services.AddScoped<IMatchRequestService, MatchRequestService>();

        return services;
    }
}