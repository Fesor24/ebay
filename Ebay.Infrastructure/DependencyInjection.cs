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
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Typesense.Setup;

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

        ConfigureTypeSense(services, configuration);

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

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = ctx =>
                    {
                        var logger = ctx.HttpContext.RequestServices
                            .GetRequiredService<ILoggerFactory>()
                            .CreateLogger("JwtAuthentication");

                        logger.LogError(ctx.Exception,
                            "JWT authentication failed: {Message}", ctx.Exception.Message);

                        return Task.CompletedTask;
                    },

                    OnTokenValidated = ctx =>
                    {
                        var logger = ctx.HttpContext.RequestServices
                            .GetRequiredService<ILoggerFactory>()
                            .CreateLogger("JwtAuthentication");

                        logger.LogInformation(
                            "JWT successfully validated for user: {User}",
                            ctx.Principal?.Identity?.Name ?? "Unknown"
                            );

                        return Task.CompletedTask;
                    },

                    OnChallenge = ctx =>
                    {
                        var logger = ctx.HttpContext.RequestServices
                            .GetRequiredService<ILoggerFactory>()
                            .CreateLogger("JwtAuthentication");

                        logger.LogWarning(
                            "JWT Challenge: Error={Error}, Description={Description}",
                            ctx.Error,
                            ctx.ErrorDescription
                            );

                        return Task.CompletedTask;
                    }


                };
            });

        services.AddAuthorization();
    }

    public static void AddOptions(IServiceCollection services, IConfiguration config)
    {
        services.Configure<JwtSettings>(config.GetSection(JwtSettings.NAME));

    }

    public static void ConfigureTypeSense(IServiceCollection services, IConfiguration config)
    {
        string apiKey = config["Typesense:ApiKey"] ?? 
            throw new ArgumentNullException("Typesense API key missing");
        string host = config["Typesense:Host"] ?? "localhost";
        string port = config["Typesense:Port"] ?? "8108";
        string protocol = config["Typesense:Protocol"] ?? "http";


        services.AddTypesenseClient(cfg =>
        {
            cfg.ApiKey = apiKey;
            cfg.Nodes = new List<Node>
            {
                new Node(host, port, protocol)
            };
        }, enableHttpCompression: false);
    }
}
