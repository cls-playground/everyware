
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
**** Last Changed On:   26/08/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

#region Usings

using Elseware.Shared.Extensions;

#endregion Usings

namespace Elseware.Host;

/// <summary>
/// Represents the entry point for the API application.
/// </summary>
public class Program {

    #region Public Static Methods

    /// <summary>
    /// Starts the API application.
    /// </summary>
    /// <param name="p_Arguments">
    /// A <see cref="String"/> array representing command line arguments passed to the application API.
    /// </param>
    public static void Main(
        String[] p_Arguments) {

        var l_WebApplicationBuilder = WebApplication.CreateBuilder(p_Arguments);
        l_WebApplicationBuilder.SetupServices();

        var l_WebApplication = l_WebApplicationBuilder.Build();
        l_WebApplication.ConfigureMiddlewares();
        l_WebApplication.UpdateDatabase();

        // Starts the application.
        l_WebApplication.Run();
    }

    #endregion Public Static Methods
}
