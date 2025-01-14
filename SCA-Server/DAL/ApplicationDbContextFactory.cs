using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using DAL;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args = null) // Optional parameter args
    {
        // Build configuration for design-time context creation
        var connectionString = "Server=10.10.5.3;Database=sca;User Id=scaremote;Password=YellowRed63!;"; // "Server=localhost;Database=sca;User Id=root;Password=password;"

		var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}