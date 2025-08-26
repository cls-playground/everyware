
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            RoleConfiguration.cs
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
/// Configures the database schema for the <see cref="Role"/> entity.
/// </summary>
public class RoleConfiguration : IEntityTypeConfiguration<Role> {

    #region Public Methods

    /// <summary>
    /// Configures the <see cref="Role"/> entity mapping using Fluent API.
    /// </summary>
    /// <param name="p_EntityType">
    /// An <see cref="EntityTypeBuilder{Role}"/> used to define the entity schema for the <see cref="Role"/> entity.
    /// </param>
    public void Configure(
        EntityTypeBuilder<Role> p_EntityType) {

        p_EntityType.ToTable("Role");
        p_EntityType.HasKey(p_Entity => p_Entity.Id);

        p_EntityType.Property(p_Entity => p_Entity.Id)
            .HasColumnName("KEY_Role")
            .HasColumnType("uniqueidentifier");

        p_EntityType.Property(p_Entity => p_Entity.Name)
            .HasColumnName("IDN_Role")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

        p_EntityType.Property(p_Entity => p_Entity.NormalizedName)
            .HasColumnName("TXT_NormalizedName")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

        p_EntityType.Property(p_Entity => p_Entity.ConcurrencyStamp)
            .HasColumnName("TXT_ConcurrencyStamp")
            .HasColumnType("nvarchar(max)");
    }

    #endregion Public Methods
}
