
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            WebApplicationExtender.cs
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        24/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   26/08/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

#region Usings

using Elseware.Infrastructure.Persistence;
using Elseware.Middlewares;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Text.Json;

#endregion Usings

namespace Elseware.Shared.Extensions;

/// <summary>
/// Extends the behavior of the <see cref="WebApplication"/> class.
/// </summary>
public static class WebApplicationExtender {

    #region Public Static Methods

    /// <summary>
    /// Adds required middlewares to the pipeline of the application API.
    /// </summary>
    /// <param name="p_This">
    /// A <see cref="WebApplication"/> instance representing the collection to which services will be added.
    /// </param>
    public static void ConfigureMiddlewares(
        this WebApplication p_This) {

        // Rejects incoming HTTP requests to enforce HTTPS-only access
        p_This.Use(
            async (p_Context, p_Next) => {
                if (p_Context.Request.IsHttps is false) {
                    p_Context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await p_Context.Response.WriteAsync("Secure connection required: please use HTTPS.");
                    return;
                }

                await p_Next();
            });

        // Applies security headers in the production environment.
        if (p_This.Environment.IsProduction()) {
            p_This.UseMiddleware<SecurityHeadersMiddleware>();
        }

        // Configures the HTTP request pipeline for the development environment.
        if (p_This.Environment.IsDevelopment()) {
            p_This.MapOpenApi();
            p_This.MapScalarApiReference();
        }

        // Configures middleware pipeline.
        p_This.UseCors("ElsewareCorsPolicy");
        p_This.UseAuthentication();
        p_This.UseAuthorization();
        p_This.MapControllers();

        p_This.MapHealthChecks(
            "/health/config"
        ,   new HealthCheckOptions {
            ResponseWriter = async (context, report) => {
                context.Response.ContentType = "application/json";
                var l_Result = JsonSerializer.Serialize(
                        new {
                            status = report.Status.ToString(),
                            checks =
                                    report.Entries.Select(
                                        e => new {
                                            name = e.Key,
                                            status = e.Value.Status.ToString(),
                                            error = e.Value.Exception?.Message
                                        })
                        });

                await context.Response.WriteAsync(l_Result);
            }
        });
    }

    /// <summary>
    /// Applies pending migrations to the application database.
    /// </summary>
    /// <param name="p_This">
    /// A <see cref="WebApplication"/> instance representing the collection to which services will be added.
    /// </param>
    public static void UpdateDatabase(
        this WebApplication p_This) {

        // Applies pending database migrations.
        try {
            using var l_Scope = p_This.Services.CreateScope();
            var l_Datastore = l_Scope.ServiceProvider.GetRequiredService<Datastore>();
            if (l_Datastore.Database.GetPendingMigrations().Any()) {

                // Migrations are applied only if the database is out of date.
                l_Datastore.Database.Migrate();
            }
        }
        catch (Exception l_Exception) {

            // TODO:    Use SERILOG to log errors during startup...

            Console.WriteLine($"Errore durante la migrazione: {l_Exception.Message}");
            throw;
        }
    }

    #endregion Public Static Methods
}
