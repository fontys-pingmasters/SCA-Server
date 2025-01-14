using Business.Implementations;
using Business.Repositories;
using Business.Services;
using DAL.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDAL(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMatchRequestRepository, MatchRequestRepository>();
        services.AddScoped<IMatchRepository, MatchRepository>();
        services.AddScoped<IEloHistoryRepository, EloHistoryRepository>();

        return services;
    }
}