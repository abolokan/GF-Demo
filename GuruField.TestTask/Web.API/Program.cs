using Application.Common;
using Persistence.Common;
using Infrastructure.Common;
using InfrastructureAssembly = Infrastructure.Common.AssemblyRefernce;
using PersistenceAssembly = Persistence.Common.AssemblyRefernce;

namespace Web.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .Scan(
            selector => selector
            .FromAssemblies(InfrastructureAssembly.Assembly, PersistenceAssembly.Assembly)
            .AddClasses(false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        builder.Services.AddPersistence(builder.Configuration);
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure();

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.Run();
    }
}
