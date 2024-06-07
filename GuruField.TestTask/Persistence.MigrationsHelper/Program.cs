using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Persistence.MigrationsHelper;

internal class Program
{
    static async Task Main(string[] args)
    {
        var hostBuilder = Host.CreateDefaultBuilder(args);

        hostBuilder.ConfigureAppConfiguration(configBuilder =>
        {
            configBuilder.SetBasePath(AppContext.BaseDirectory);
            configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        });

        hostBuilder.ConfigureServices((context, services) =>
        {
            var connectionString = context.Configuration.GetConnectionString("DefaultConnection") ??
                throw new KeyNotFoundException("Could not find DB connection string...");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        });

        var host = hostBuilder.Build();
        var dbContext = host.Services.GetRequiredService<ApplicationDbContext>();

        if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
        {
            await dbContext.Database.MigrateAsync();
            Console.WriteLine("Migrations applied");
        }
        else Console.WriteLine("No migrations");
    }
}
