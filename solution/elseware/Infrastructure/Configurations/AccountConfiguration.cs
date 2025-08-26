
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            AccountConfiguration.cs
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
/// Configures the database schema for the <see cref="Account"/> entity.
/// </summary>
public class AccountConfiguration : IEntityTypeConfiguration<Account> {

    #region Public Methods

    /// <summary>
    /// Configures the <see cref="Account"/> entity mapping using Fluent API.
    /// </summary>
    /// <param name="p_EntityType">
    /// An <see cref="EntityTypeBuilder{Account}"/> instance used to define the entity schema for the entity <see cref="Account"/>.
    /// </param>
    public void Configure(
        EntityTypeBuilder<Account> p_EntityType) {

        p_EntityType.ToTable("Account");
        p_EntityType.HasKey(p_Entity => p_Entity.Id);

        p_EntityType.Property(p_Entity => p_Entity.Id)
            .HasColumnName("KEY_Account")
            .HasColumnType("uniqueidentifier");

        p_EntityType.Property(p_Entity => p_Entity.UserName)
            .HasColumnName("IDN_Account")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

        p_EntityType.Property(p_Entity => p_Entity.NormalizedUserName)
            .HasColumnName("TXT_NormalizedUserName")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

        p_EntityType.Property(p_Entity => p_Entity.Email)
            .HasColumnName("TXT_Email")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

        p_EntityType.Property(p_Entity => p_Entity.NormalizedEmail)
            .HasColumnName("TXT_NormalizedEmail")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

        p_EntityType.Property(p_Entity => p_Entity.EmailConfirmed)
            .HasColumnName("BLN_EmailConfirmed")
            .HasColumnType("bit")
            .IsRequired();

        p_EntityType.Property(p_Entity => p_Entity.PasswordHash)
            .HasColumnName("TXT_PasswordHash")
            .HasColumnType("nvarchar(max)");

        p_EntityType.Property(p_Entity => p_Entity.SecurityStamp)
            .HasColumnName("TXT_SecurityStamp")
            .HasColumnType("nvarchar(max)");

        p_EntityType.Property(p_Entity => p_Entity.ConcurrencyStamp)
            .HasColumnName("TXT_ConcurrencyStamp")
            .HasColumnType("nvarchar(max)")
            .IsConcurrencyToken();

        p_EntityType.Property(p_Entity => p_Entity.PhoneNumber)
            .HasColumnName("TXT_PhoneNumber")
            .HasColumnType("nvarchar(max)");

        p_EntityType.Property(p_Entity => p_Entity.PhoneNumberConfirmed)
            .HasColumnName("BLN_PhoneNumberConfirmed")
            .HasColumnType("bit")
            .IsRequired();

        p_EntityType.Property(p_Entity => p_Entity.TwoFactorEnabled)
            .HasColumnName("BLN_TwoFactorEnabled")
            .HasColumnType("bit")
            .IsRequired();

        p_EntityType.Property(p_Entity => p_Entity.LockoutEnd)
            .HasColumnName("DTE_LockoutEnd")
            .HasColumnType("datetimeoffset");

        p_EntityType.Property(p_Entity => p_Entity.LockoutEnabled)
            .HasColumnName("BLN_LockoutEnabled")
            .HasColumnType("bit")
            .IsRequired();

        p_EntityType.Property(p_Entity => p_Entity.AccessFailedCount)
            .HasColumnName("INT_AccessFailedCount")
            .HasColumnType("int")
            .IsRequired();

        p_EntityType.Property(p_Entity => p_Entity.FirstName)
            .HasColumnName("TXT_FirstName")
            .HasColumnType("nvarchar(64)")
            .HasMaxLength(64);

        p_EntityType.Property(p_Entity => p_Entity.LastName)
            .HasColumnName("TXT_Surname")
            .HasColumnType("nvarchar(64)")
            .HasMaxLength(64);

        p_EntityType.Property(p_Entity => p_Entity.BirthDate)
            .HasColumnName("DTE_Birth")
            .HasColumnType("datetime");
    }

    #endregion Public Methods
}
