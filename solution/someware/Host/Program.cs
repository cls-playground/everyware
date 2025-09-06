
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Someware
****
**** Module:            Program.cs
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        27/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   03/09/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

#region Usings

using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Someware.Authentication;
using Someware.Services.Authentication;
using Someware.Services.Navigation;
using Someware.Shared.Extensions;

#endregion Usings

namespace Someware.Host;

/// <summary>
/// Provides the application entry point and startup logic for the Blazor WebAssembly host.
/// </summary>
public class Program {

    #region Public Static Async Methods

    /// <summary>
    /// Configures services, registers root components and builds the application host.
    /// </summary>
    /// <param name="p_Arguments">
    /// A <see cref="String"/> array representing command-line arguments passed to the application.
    /// These are not used in Blazor WebAssembly as the application runs inside a browser.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> instance representing the asynchronous operation of launching the application.
    /// </returns>
    /// <remarks>
    /// Represents the entry point of the Blazor WebAssembly application.
    /// </remarks>    
    public static async Task Main(
        String[] p_Arguments) {

        var l_WebAssemblyHostBuilder = WebAssemblyHostBuilder.CreateDefault(p_Arguments);

        // Retrieves the "project" parameter from JavaScript.
        var l_ProjectName = await l_WebAssemblyHostBuilder.GetProjectName();

        // Enables structured logging for diagnostics and telemetry.
        l_WebAssemblyHostBuilder.Logging.SetMinimumLevel(LogLevel.Information);

        l_WebAssemblyHostBuilder.RootComponents.Add<App>("#app");
        l_WebAssemblyHostBuilder.RootComponents.Add<HeadOutlet>("head::after");

        l_WebAssemblyHostBuilder.Services.AddAuthorizationCore();
        l_WebAssemblyHostBuilder.Services.AddBlazoredLocalStorage();
        l_WebAssemblyHostBuilder.Services.AddScoped<AuthenticationService>();
        l_WebAssemblyHostBuilder.Services.AddScoped<AuthenticationStateProvider>(p_ServiceProvider => p_ServiceProvider.GetRequiredService<JwtAuthenticationStateProvider>());
        l_WebAssemblyHostBuilder.Services.AddScoped<JwtAuthenticationStateProvider>();
        l_WebAssemblyHostBuilder.Services.AddScoped<RedirectService>();

        // Loads configuration based on the project name.
        var l_Configuration = await l_WebAssemblyHostBuilder.LoadConfigurationAsync(l_ProjectName);

        // Registers the HttpClient for API communication.
        var l_Environment = l_WebAssemblyHostBuilder.HostEnvironment.Environment;
        var l_ApiBaseUrl = l_Configuration[$"ApiBaseUrl:{l_Environment}"];
        if (String.IsNullOrWhiteSpace(l_ApiBaseUrl)) {
            throw new InvalidOperationException($"ApiBaseUrl is missing for '{l_Environment}' environment.");
        }

        l_WebAssemblyHostBuilder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(l_ApiBaseUrl) });

        // Builds the host to access registered services.
        var l_Host = l_WebAssemblyHostBuilder.Build();

        // Runs the application.
        await l_Host.RunAsync();
    }

    #endregion Public Static Async Methods
}
