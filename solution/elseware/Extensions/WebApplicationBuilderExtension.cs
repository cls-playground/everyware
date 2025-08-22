
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
**** Created On:        21-08-2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   21-08-2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

namespace Elseware.Extensions;

/// <summary>
/// Extends the behavior of the <see cref="WebApplicationBuilder"/> type.
/// </summary>
public static class WebApplicationBuilderExtension {

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
    public static IServiceCollection ConfigureCors(
        this WebApplicationBuilder p_This)

    =>  p_This.Services.AddCors(
            p_Options =>
                p_Options.AddPolicy(
                    "CorsPolicy"
                ,   p_Policy => {
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
}
