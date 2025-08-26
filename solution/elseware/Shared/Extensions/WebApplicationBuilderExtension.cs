
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            WebApplicationBuilderExtension
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        21/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   26/08/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

#region Usings

using Elseware.Application.Interfaces;
using Elseware.Application.Services;
using Elseware.Diagnostics;
using Elseware.Domain;
using Elseware.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;
using System.Text;

#endregion Usings

namespace Elseware.Shared.Extensions;

/// <summary>
/// Extends the behavior of the <see cref="WebApplicationBuilder"/> type.
/// </summary>
public static class WebApplicationBuilderExtension {

    #region Public Static Methods

    /// <summary>
    /// Executes the full application setup pipeline by chaining configuration, service registration, authentication, identity, routing
    /// and custom services.
    /// </summary>
    /// <param name="p_This">
    /// A <see cref="WebApplicationBuilder"/> instance representing the builder to be configured.
    /// </param>
    /// <returns>
    /// A <see cref="WebApplicationBuilder"/> instance representing the configured builder.
    /// </returns>
    public static WebApplicationBuilder SetupServices(
        this WebApplicationBuilder p_This)

    => p_This
            .LoadConfiguration()
            .ConfigureServices()
            .ConfigureCors()
            .ConfigureDatabase()
            .ConfigureIdentity()
            .ConfigureAuthentication()
            .ConfigureRouting()
            .ConfigureCustomServices();

    /// <summary>
    /// Configures JWT-based authentication using settings from the application configuration and environment variables.
    /// </summary>
    /// <param name="p_This">The <see cref="WebApplicationBuilder"/> instance to configure.</param>
    /// <returns>The configured <see cref="WebApplicationBuilder"/> instance.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the JWT secret key is missing from environment variables.
    /// </exception>
    /// <remarks>
    /// This method sets up token validation parameters including issuer, audience, lifetime, and signing key.
    /// </remarks>
    private static WebApplicationBuilder ConfigureAuthentication(
        this WebApplicationBuilder p_This) {

        var l_JwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
        if (String.IsNullOrWhiteSpace(l_JwtSecretKey)) {
            throw new InvalidOperationException($"Startup failure: the JWT secret key is missing.");
        }

        p_This.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                p_Options => 
                    p_Options.TokenValidationParameters =
                        new TokenValidationParameters {
                            ValidateIssuer = true
                        ,   ValidIssuer = p_This.Configuration.GetSection("Jwt:Issuer").Get<String>()
                        ,   ValidateAudience = true
                        ,   ValidAudience = p_This.Configuration.GetSection("Jwt:Audience").Get<String>()
                        ,   ValidateLifetime = true
                        ,   ValidateIssuerSigningKey = true
                        ,   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(l_JwtSecretKey))
                        ,   RequireExpirationTime = true
                        ,   ClockSkew = TimeSpan.Zero
                        });

        return p_This;
    }

    /// <summary>
    /// Configures the Cross Origin policy for the web API.
    /// </summary>
    /// <param name="p_This">
    /// A <see cref="WebApplicationBuilder"/> instance representing the builder used to configure services and middleware.
    /// </param>
    /// <returns>
    /// An <see cref="IServiceCollection"/> instance allowing further service registrations to be chained.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the 'Cors:AllowedOrigins' configuration section is missing or empty.
    /// This prevents the application from initializing the CORS policy correctly.
    /// Ensure that the environment-specific settings file includes a valid list of allowed origins.
    /// </exception>
    /// <remarks>
    /// This method registers a named policy called <c>"CorsPolicy"</c>, which allows requests
    /// from origins defined in the application configuration under the <c>Cors:AllowedOrigins</c> section.
    /// </remarks>
    private static WebApplicationBuilder ConfigureCors(
        this WebApplicationBuilder p_This) {

        p_This.Services.AddCors(
            p_Options =>
                p_Options.AddPolicy(
                    "CorsPolicy"
                , p_Policy => {
                    var l_AllowedOrigins = p_This.Configuration.GetSection("Cors:AllowedOrigins").Get<String[]>();
                    if (l_AllowedOrigins is null || l_AllowedOrigins.Length == 0) {
                        throw new InvalidOperationException(
                            "Startup failure: unable to initialize CORS policy due to missing or undefined configuration.");
                    }

                    p_Policy
                        .WithOrigins(l_AllowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                }));

        return p_This;
    }

    /// <summary>
    /// Registers custom application services required by the business logic layer.
    /// </summary>
    /// <param name="p_This">The <see cref="WebApplicationBuilder"/> instance to configure.</param>
    /// <returns>The configured <see cref="WebApplicationBuilder"/> instance.</returns>
    private static WebApplicationBuilder ConfigureCustomServices(
        this WebApplicationBuilder p_This) {

        p_This.Services.AddScoped<IJwtService, JwtService>();
        return p_This;
    }

    /// <summary>
    /// Configures the Entity Framework Core database context using SQL Server and custom migration settings.
    /// </summary>
    /// <param name="p_This">The <see cref="WebApplicationBuilder"/> instance to configure.</param>
    /// <returns>The configured <see cref="WebApplicationBuilder"/> instance.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the connection string is missing or empty in the configuration.
    /// </exception>
    /// <remarks>
    /// This method also enables sensitive data logging and replaces the default migration history repository.
    /// </remarks>
    private static WebApplicationBuilder ConfigureDatabase(
        this WebApplicationBuilder p_This) {

        var l_ConnectionString = p_This.Configuration.GetSection("Datastore:ConnectionString").Get<String>();
        if (String.IsNullOrWhiteSpace(l_ConnectionString)) {
            throw new InvalidOperationException("Startup failure: the connection string is missing.");
        }

        p_This.Services.AddDbContext<Datastore>(
            p_DbContextOptions =>
                p_DbContextOptions
                    .UseSqlServer(
                        l_ConnectionString
                    ,   p_SqlServerOptions => p_SqlServerOptions.MigrationsHistoryTable("DatastoreMigrations", "dbo"))
                
                    .ReplaceService<IHistoryRepository, DatastoreMigration>()

                    .EnableSensitiveDataLogging()
                    .LogTo(Console.WriteLine, LogLevel.Information));

        return p_This;
    }

    /// <summary>
    /// Registers ASP.NET Core Identity services using the configured database context.
    /// </summary>
    /// <param name="p_This">The <see cref="WebApplicationBuilder"/> instance to configure.</param>
    /// <returns>The configured <see cref="WebApplicationBuilder"/> instance.</returns>
    /// <remarks>
    /// Adds default token providers and links Identity to the Entity Framework store.
    /// </remarks>
    private static WebApplicationBuilder ConfigureIdentity(
        this WebApplicationBuilder p_This) { 

        p_This.Services
            .AddIdentity<Account, Role>()
            .AddEntityFrameworkStores<Datastore>()
            .AddDefaultTokenProviders();

        return p_This;
    }

    /// <summary>
    /// Configures routing options for the application, enforcing lowercase URLs.
    /// </summary>
    /// <param name="p_This">The <see cref="WebApplicationBuilder"/> instance to configure.</param>
    /// <returns>The configured <see cref="WebApplicationBuilder"/> instance.</returns>
    private static WebApplicationBuilder ConfigureRouting(
        this WebApplicationBuilder p_This) { 

        p_This.Services.AddRouting(options => options.LowercaseUrls = true);
        return p_This;
    }

    /// <summary>
    /// Registers core framework services including health checks, controllers, and OpenAPI documentation.
    /// </summary>
    /// <param name="p_This">The <see cref="WebApplicationBuilder"/> instance to configure.</param>
    /// <returns>The configured <see cref="WebApplicationBuilder"/> instance.</returns>
    private static WebApplicationBuilder ConfigureServices(
        this WebApplicationBuilder p_This) {

        p_This.Services.AddHealthChecks().AddCheck("Configuration", new ConfigurationHealthCheck());
        p_This.Services.AddControllers();
        p_This.Services.AddOpenApi();
        return p_This;
    }

    /// <summary>
    /// Loads environment-specific configuration settings into the application builder.
    /// </summary>
    /// <param name="p_This">The <see cref="WebApplicationBuilder"/> instance to configure.</param>
    /// <returns>The configured <see cref="WebApplicationBuilder"/> instance.</returns>
    private static WebApplicationBuilder LoadConfiguration(
        this WebApplicationBuilder p_This) {

        p_This.Configuration.LoadConfiguration();
        return p_This;
    }

    #endregion Public Static Methods
}
