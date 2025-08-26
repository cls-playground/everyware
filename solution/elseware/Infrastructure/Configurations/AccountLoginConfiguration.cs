
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            AccountLoginConfiguration.cs
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
/// Configures the database schema for the <see cref="AccountLogin"/> entity.
/// </summary>
public class AccountLoginConfiguration : IEntityTypeConfiguration<AccountLogin> {

    #region Public Methods

    /// <summary>
    /// Configures the <see cref="AccountLogin"/> entity mapping using Fluent API.
    /// </summary>
    /// <param name="p_EntityType">
    /// An <see cref="EntityTypeBuilder{AccountLogin}"/> used to define the entity schema for the <see cref="AccountLogin"/> entity.
    /// </param>
    public void Configure(
        EntityTypeBuilder<AccountLogin> p_EntityType) {

        p_EntityType.ToTable("AccountLogin");
        p_EntityType.HasKey(p_Entity => new { p_Entity.LoginProvider, p_Entity.ProviderKey });

        p_EntityType.Property(p_Entity => p_Entity.LoginProvider)
            .HasColumnName("TXT_Provider")
            .HasColumnType("nvarchar(128)")
            .HasMaxLength(128);

        p_EntityType.Property(p_Entity => p_Entity.ProviderKey)
            .HasColumnName("TXT_Key")
            .HasColumnType("nvarchar(128)")
            .HasMaxLength(128);

        p_EntityType.Property(p_Entity => p_Entity.ProviderDisplayName)
            .HasColumnName("TXT_DisplayName")
            .HasColumnType("nvarchar(max)");

        p_EntityType.Property(p_Entity => p_Entity.UserId)
            .HasColumnName("LNK_Account")
            .HasColumnType("uniqueidentifier")
            .IsRequired();
    }

    #endregion Public Methods
}
