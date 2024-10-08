using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDAL(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("InMemoryDb"));

        services.AddScoped<UserRepository>();

        return services;
    }
}