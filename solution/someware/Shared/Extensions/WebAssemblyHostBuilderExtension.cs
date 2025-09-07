
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Someware
****
**** Module:            WebAssemblyHostBuilderExtendion
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        03/09/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   03/09/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

#region Usings

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

#endregion Usings

namespace Someware.Shared.Extensions;

/// <summary>
/// Extends the behavior of the <see cref="WebAssemblyHostBuilder"/> class.
/// </summary>
public static class WebAssemblyHostBuilderExtension {

    #region Public Static Async Methods

    /// <summary>
    /// Loads the application configuration from a JSON file located in the specified project folder.
    /// </summary>
    /// <param name="p_This">
    /// The current <see cref="WebAssemblyHostBuilder"/> instance on which this extension method is called.
    /// </param>
    /// <param name="p_ProjectName">
    /// A <see cref="String"/> representing the name of the project whose configuration should be loaded.
    /// </param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> whose result is the updated <see cref="IConfiguration"/> instance
    /// containing the loaded project-specific settings.
    /// </returns>
    /// <remarks>
    /// This method retrieves the <c>project.json</c> file from the <c>projects/{ProjectName}/settings</c> directory
    /// and merges its contents into the application's configuration.
    /// </remarks>
    public static async Task<IConfiguration> LoadConfigurationAsync(
        this WebAssemblyHostBuilder p_This
    ,   String p_ProjectName) {

        using var l_LocalHttp = new HttpClient();
        l_LocalHttp.BaseAddress = new(p_This.HostEnvironment.BaseAddress);

        using var l_Stream = await l_LocalHttp.GetStreamAsync($"projects/{p_ProjectName}/settings/project.json");
        p_This.Configuration.AddJsonStream(l_Stream);
        return p_This.Configuration;
    }

    /// <summary>
    /// Retrieves the project name from the JavaScript runtime by invoking the <c>getProjectName</c> function.
    /// </summary>
    /// <param name="p_This">
    /// The current <see cref="WebAssemblyHostBuilder"/> instance on which this extension method is called.
    /// </param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> whose result is a <see cref="String"/> containing the project name.
    /// </returns>
    /// <remarks>
    /// This method builds a temporary host to access <see cref="IJSRuntime"/> and calls the JavaScript
    /// function <c>getProjectName</c> to obtain the value.
    /// </remarks>
    public static async Task<String> GetProjectName(
        this WebAssemblyHostBuilder p_This) {

        var l_Host = p_This.Build();
        var js = l_Host.Services.GetRequiredService<IJSRuntime>();
        return await js.InvokeAsync<String>("getProjectName");
    }

    #endregion Public Static Async Methods
}
