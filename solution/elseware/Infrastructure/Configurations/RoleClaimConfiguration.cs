
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            RoleClaimConfiguration.cs
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
/// Configures the database schema for the <see cref="RoleClaim"/> entity.
/// </summary>
public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim> {

    #region Public Methods

    /// <summary>
    /// Configures the <see cref="RoleClaim"/> entity mapping using Fluent API.
    /// </summary>
    /// <param name="p_EntityType">
    /// An <see cref="EntityTypeBuilder{RoleClaim}"/> used to define the entity schema for the <see cref="RoleClaim"/> entity.
    /// </param>
    public void Configure(
        EntityTypeBuilder<RoleClaim> p_EntityType) {

        p_EntityType.ToTable("RoleClaim");
        p_EntityType.HasKey(p_Entity => p_Entity.Id);

        p_EntityType.Property(p_Entity => p_Entity.Id)
            .HasColumnName("INT_Claim")
            .HasColumnType("int");

        p_EntityType.Property(p_Entity => p_Entity.RoleId)
            .HasColumnName("LNK_Role")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        p_EntityType.Property(p_Entity => p_Entity.ClaimType)
            .HasColumnName("TXT_ClaimType")
            .HasColumnType("nvarchar(max)");

        p_EntityType.Property(p_Entity => p_Entity.ClaimValue)
            .HasColumnName("TXT_ClaimValue")
            .HasColumnType("nvarchar(max)");
    }

    #endregion Public Methods
}
