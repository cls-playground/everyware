
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            DatastoreHistoryRepository.cs
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

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.SqlServer.Migrations.Internal;
using System.Diagnostics.CodeAnalysis;

#endregion Usings

namespace Elseware.Infrastructure.Persistence.Design;

/// <summary>
/// Customizes the Entity Framework Core migration history table for the <c>Datastore</c> context.
/// </summary>
/// <param name="p_Dependencies">
/// A <see cref="HistoryRepositoryDependencies"/> instance representing the EF Core migration infrastructure dependencies.
/// </param>
/// <remarks>
/// Overrides the default <see cref="SqlServerHistoryRepository"/> behavior to rename columns and add a custom key.
/// This allows tracking of migration metadata using domain-specific naming conventions.
/// </remarks>
internal class DatastoreHistoryRepository(
    HistoryRepositoryDependencies p_Dependencies) : SqlServerHistoryRepository(p_Dependencies) {

    #region Protected Override Methods

    /// <summary>
    /// Configures the structure of the migration history table by renaming default columns and adding a custom identifier.
    /// </summary>
    /// <param name="p_EntityType">
    /// An <see cref="EntityTypeBuilder{HistoryRow}"/> instance used to define the schema of the migration history entity.
    /// </param>
    /// <remarks>
    /// Adds a custom column <c>KEY_DatastoreMigration</c> of type <c>uniqueidentifier</c> with default value <c>NEWSEQUENTIALID()</c>.
    /// Renames <c>MigrationId</c> to <c>IDN_DatastoreMigration</c> and <c>ProductVersion</c> to <c>TXT_EntityFrameworkCoreVersion</c>
    /// to align with internal naming conventions.
    /// </remarks>
    protected override void ConfigureTable(
        EntityTypeBuilder<HistoryRow> p_EntityType) {
        
        base.ConfigureTable(p_EntityType);

        // Adds the KEY column.
        p_EntityType
            .Property<Guid>("KEY_DatastoreMigration")
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("NEWSEQUENTIALID()")
            .IsRequired();

        // Renames existing columns.
        p_EntityType.Property(p_HistoryRow => p_HistoryRow.MigrationId).HasColumnName("IDN_DatastoreMigration");
        p_EntityType.Property(p_HistoryRow => p_HistoryRow.ProductVersion).HasColumnName("TXT_EntityFrameworkCoreVersion");
    }

    #endregion Protected Override Methods
}
