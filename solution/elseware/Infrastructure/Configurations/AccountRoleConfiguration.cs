
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            AccountRoleConfiguration.cs
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        25/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   25/08/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

#region Usings

using Elseware.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion Usings

namespace Elseware.Infrastructure.Configurations;

/// <summary>
/// Configures the database schema for the <see cref="AccountRole"/> entity.
/// </summary>
public class AccountRoleConfiguration : IEntityTypeConfiguration<AccountRole> {

    #region Public Methods

    /// <summary>
    /// Configures the <see cref="AccountRole"/> entity mapping using Fluent API.
    /// </summary>
    /// <param name="p_EntityType">
    /// An <see cref="EntityTypeBuilder{AccountRole}"/> used to define the entity schema for the <see cref="AccountRole"/> entity.
    /// </param>
    public void Configure(
        EntityTypeBuilder<AccountRole> p_EntityType) {

        p_EntityType.ToTable("AccountRole");
        p_EntityType.HasKey(p_Entity => new { p_Entity.UserId, p_Entity.RoleId });

        p_EntityType.Property(p_Entity => p_Entity.UserId)
            .HasColumnName("LNK_Account")
            .HasColumnType("uniqueidentifier");

        p_EntityType.Property(p_Entity => p_Entity.RoleId)
            .HasColumnName("LNK_Role")
            .HasColumnType("uniqueidentifier");
    }

    #endregion Public Methods
}
