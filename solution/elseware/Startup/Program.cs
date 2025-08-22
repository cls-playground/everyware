
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            Program.cs
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        19/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   23/08/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

#region Usings

using Elseware.Extensions;
using Elseware.Middlewares;
using Scalar.AspNetCore;

#endregion Usings

namespace Elseware.Startup;

/// <summary>
/// Represents the entry point for the API application.
/// </summary>
public class Program {

    /// <summary>
    /// Starts the API application.
    /// </summary>
    /// <param name="p_Arguments">
    /// A <see cref="String"/> array representing command line arguments passed to the application API.
    /// </param>
    public static void Main(
        String[] p_Arguments) {

        var l_WebApplicationBuilder = WebApplication.CreateBuilder(p_Arguments);

        // Configures custom configuration file location
        l_WebApplicationBuilder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("Settings/appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(
                $"Settings/appsettings.{l_WebApplicationBuilder.Environment.EnvironmentName}.json"
            ,   optional: true
            ,   reloadOnChange: true);

        // Registers services.
        l_WebApplicationBuilder.Services.AddControllers();
        l_WebApplicationBuilder.Services.AddOpenApi();

        l_WebApplicationBuilder.ConfigureCors();

        var l_WebApplication = l_WebApplicationBuilder.Build();

        // Rejects incoming HTTP requests to enforce HTTPS-only access
        l_WebApplication.Use(
            async (p_Context, p_Next) => {
                if (p_Context.Request.IsHttps is false) {
                    p_Context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await p_Context.Response.WriteAsync("Secure connection required: please use HTTPS.");
                    return;
                }

                await p_Next();
            });

        // Applies security headers in the production environment.
        if (l_WebApplication.Environment.IsProduction()) {
            l_WebApplication.UseMiddleware<SecurityHeadersMiddleware>();
        }

        // Configures the HTTP request pipeline for the development environment.
        if (l_WebApplication.Environment.IsDevelopment()) {
            l_WebApplication.MapOpenApi();
            l_WebApplication.MapScalarApiReference();
        }

        // Configures middleware pipeline.
        l_WebApplication.UseCors("ElsewareCorsPolicy");
        l_WebApplication.UseAuthorization();
        l_WebApplication.MapControllers();

        // Starts the application.
        l_WebApplication.Run();
    }
}
