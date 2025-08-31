
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            Datastore.cs
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        23/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   25/08/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

#region Usings

using Elseware.Domain;
using Elseware.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

#endregion Usings

namespace Elseware.Infrastructure.Persistence;

/// <summary>
/// Represents the database context for the application.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Datastore"/> class.
/// </remarks>
/// <param name="p_Options">
/// A <see cref = "DbContextOptions"/> instance representing options to configure the database context.
/// </param>
public class Datastore(
    DbContextOptions<Datastore> p_Options) 
    :   IdentityDbContext<
            Account
        ,   Role
        ,   Guid
        ,   AccountClaim
        ,   AccountRole
        ,   AccountLogin
        ,   RoleClaim
        ,   AccountToken>(p_Options) {

    #region Protected Override Methods

    /// <summary>
    /// Configures the entity framework model for the current context.
    /// </summary>
    /// <param name="p_ModelBuilder">
    /// A <see cref="ModelBuilder"/> instance providing the surface for configuring the database context model.
    /// </param>
    protected override void OnModelCreating(
        ModelBuilder p_ModelBuilder) {

        base.OnModelCreating(p_ModelBuilder);

        // Table names can only be set through fluent API in the OnModelCreating override method.
        // Column names also should be set through fluent API to avoid losing standard EF Core behavior.

        // Account-related mappings.
        p_ModelBuilder.ApplyConfiguration(new AccountClaimConfiguration());
        p_ModelBuilder.ApplyConfiguration(new AccountConfiguration());
        p_ModelBuilder.ApplyConfiguration(new AccountLoginConfiguration());
        p_ModelBuilder.ApplyConfiguration(new AccountRoleConfiguration());
        p_ModelBuilder.ApplyConfiguration(new AccountTokenConfiguration());

        // Role-related mappings.
        p_ModelBuilder.ApplyConfiguration(new RoleClaimConfiguration());
        p_ModelBuilder.ApplyConfiguration(new RoleConfiguration());
    }

    #endregion Protected Override Methods
}
