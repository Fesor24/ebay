using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Ebay.Application.Abstractions.Clock;
using Ebay.Application.Abstractions.Data;
using Ebay.Application.Abstractions.Email;
using Ebay.Domain.Abstractions;
using Ebay.Infrastructure.Clock;
using Ebay.Infrastructure.Data;
using Ebay.Infrastructure.Email;
using Ebay.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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

        ConfigureAuth(services, configuration);

        AddOptions(services, configuration);

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

    public static void ConfigureAuth(IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.MapInboundClaims = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["JwtSettings:Issuer"],
                    ValidAudience = config["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"])),
                    ClockSkew = TimeSpan.Zero,
                    NameClaimType = JwtRegisteredClaimNames.Name,
                    RoleClaimType = "role"
                };
            });

        services.AddAuthorization();
    }

    public static void AddOptions(IServiceCollection services, IConfiguration config)
    {
        //JwtSettings jwtSettings = new();
        //config.GetSection(jwtSettings.NAME).Bind(jwtSettings);

        //services.ConfigureOptions<JwtSettings>();

        //services.AddOptions<JwtSettings>(JwtSettings.NAME);

        services.Configure<JwtSettings>(config.GetSection(JwtSettings.NAME));

    }
}
