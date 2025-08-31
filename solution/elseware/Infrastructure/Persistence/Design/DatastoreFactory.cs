
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            DatastoreFactory.cs
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        25/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   31/08/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

#region Usings

using Elseware.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

#endregion Usings

namespace Elseware.Infrastructure.Persistence.Design;

/// <summary>
/// Provides a design-time factory for creating instances of the <see cref="Datastore"/> database context.
/// </summary>
/// <remarks>
/// This factory is used by Entity Framework Core tools to instantiate the <see cref="Datastore"/> during design-time operations,
/// such as migrations and schema generation. It reads configuration settings from JSON files located in the <c>Settings</c> folder.
/// </remarks>
public class DatastoreFactory : IDesignTimeDbContextFactory<Datastore> {

    #region Public Methods

    /// <summary>
    /// Creates a new instance of the <see cref="Datastore"/> database context for design-time operations.
    /// </summary>
    /// <param name="p_Arguments">
    /// A <see cref="String"/> array representing command-line arguments passed by the design-time tooling.
    /// </param>
    /// <returns>
    /// A <see cref="Datastore"/> instance representing the database context ready to be used by Entity Framework Core tools.
    /// </returns>    
    public Datastore CreateDbContext(
        String[] p_Arguments) {

        var l_Configuration = new ConfigurationBuilder().LoadConfiguration();
        var l_ConnectionString = l_Configuration.GetSection("Datastore:ConnectionString").Value;
        var l_OptionsBuilder = new DbContextOptionsBuilder<Datastore>();
        l_OptionsBuilder.UseSqlServer(l_ConnectionString);
        return new Datastore(l_OptionsBuilder.Options);
    }

    #endregion Public Methods
}
