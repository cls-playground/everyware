
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            DatastoreDesignTimeServices.cs
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        29/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   31/08/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

#region Usings

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations.Design;

#endregion Usings

namespace Elseware.Infrastructure.Persistence.Design;

/// <summary>
/// Registers <see cref="DatastoreMigrationsGenerator"/> as the implementation for <see cref="IMigrationsCodeGenerator"/>.
/// </summary>
public class DatastoreDesignTimeServices : IDesignTimeServices {

    #region Public Methods

    /// <summary>
    /// Registers <see cref="DatastoreMigrationsGenerator"/> as a singleton service for <see cref="IMigrationsCodeGenerator"/>.
    /// </summary>
    /// <param name="p_Services">
    /// <see cref="IServiceCollection"/> instance used to configure design-time dependencies.
    /// </param>
    public void ConfigureDesignTimeServices(
        IServiceCollection p_Services)

    =>  p_Services.AddSingleton<IMigrationsCodeGenerator, DatastoreMigrationsGenerator>();

    #endregion Public Methods
}
