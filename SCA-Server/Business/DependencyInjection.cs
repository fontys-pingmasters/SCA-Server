using Business.Implementations;
using Business.Services;
using DAL;
using Microsoft.Extensions.DependencyInjection;

namespace Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddScoped<UserService, UserServiceImpl>();
        services.AddDAL();

        return services;
    }
    
}