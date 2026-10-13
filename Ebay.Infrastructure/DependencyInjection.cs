using Ebay.Application.Abstractions.Clock;
using Ebay.Application.Abstractions.Data;
using Ebay.Application.Abstractions.Email;
using Ebay.Domain.Abstractions;
using Ebay.Infrastructure.Clock;
using Ebay.Infrastructure.Data;
using Ebay.Infrastructure.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ebay.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        services.AddTransient<IEmailService, EmailService>();

        var connectionString = configuration.GetConnectionString("Database") ??
            throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        AddScopedServices(services);

        return services;
    }

    public static void AddScopedServices(IServiceCollection services)
    {
        var assembly = typeof(ApplicationDbContext).Assembly;

        var types = assembly.GetTypes();

        var implementations = types
            .Where(type => type.IsClass &&
            !type.IsInterface &&
            !type.IsGenericTypeDefinition);

        foreach(var implementation in implementations)
        {
            var serviceInterfaces = implementation.GetInterfaces()
                .Where(i => i != typeof(IScopedService) &&
                typeof(IScopedService).IsAssignableFrom(i));

            foreach(var serviceInterface in serviceInterfaces)
            {
                services.AddScoped(serviceInterface, implementation);
            }
        }
    }
}
