using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace Persistence.Common;

public static class DependencyInjection
{
    private const int CommandTimeoutMinutes = 5;

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var filterOptions = new LoggerFilterOptions { MinLevel = LogLevel.Information };
        var myLoggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() }, filterOptions);

        services
            .AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseLoggerFactory(myLoggerFactory);

                options.UseSqlServer(connectionString,
                    o => o.CommandTimeout(CommandTimeoutMinutes * 60));
            }, ServiceLifetime.Scoped);

        return services;
    }
}
