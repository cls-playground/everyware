
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            AccountTokenConfiguration.cs
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
/// Configures the database schema for the <see cref="AccountToken"/> entity.
/// </summary>
public class AccountTokenConfiguration : IEntityTypeConfiguration<AccountToken> {

    #region Public Methods

    /// <summary>
    /// Configures the <see cref="AccountToken"/> entity mapping using Fluent API.
    /// </summary>
    /// <param name="p_EntityType">
    /// An <see cref="EntityTypeBuilder{AccountToken}"/> used to define the entity schema for the <see cref="AccountToken"/> entity.
    /// </param>
    public void Configure(
        EntityTypeBuilder<AccountToken> p_EntityType) {

        p_EntityType.ToTable("AccountToken");
        p_EntityType.HasKey(p_Entity => new { p_Entity.UserId, p_Entity.LoginProvider, p_Entity.Name });

        p_EntityType.Property(p_Entity => p_Entity.UserId)
            .HasColumnName("LNK_Account")
            .HasColumnType("uniqueidentifier");

        p_EntityType.Property(p_Entity => p_Entity.LoginProvider)
            .HasColumnName("TXT_LoginProvider")
            .HasColumnType("nvarchar(128)")
            .HasMaxLength(128);

        p_EntityType.Property(p_Entity => p_Entity.Name)
            .HasColumnName("TXT_TokenName")
            .HasColumnType("nvarchar(128)")
            .HasMaxLength(128);

        p_EntityType.Property(p_Entity => p_Entity.Value)
            .HasColumnName("TXT_TokenValue")
            .HasColumnType("nvarchar(max)");
    }

    #endregion Public Methods
}
