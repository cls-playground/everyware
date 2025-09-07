
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            DatastoreMigrationsGenerator.cs
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

using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

#endregion Usings

namespace Elseware.Infrastructure.Persistence.Design;

/// <summary>
/// Extends <see cref="CSharpMigrationsGenerator"/> to prepend standardized headers to generated migration and snapshot files.
/// </summary>
/// <param name="p_Dependencies">
/// A <see cref="MigrationsCodeGeneratorDependencies"/> instance that provides general migration generation dependencies.
/// </param>
/// <param name="p_LanguageDependencies">
/// A <see cref="CSharpMigrationsGeneratorDependencies"/> instance that provides C#-specific code generation dependencies.
/// </param>
public class DatastoreMigrationsGenerator(
    MigrationsCodeGeneratorDependencies p_Dependencies
,   CSharpMigrationsGeneratorDependencies p_LanguageDependencies) : CSharpMigrationsGenerator(p_Dependencies, p_LanguageDependencies) {

    #region Public Override Methods

    /// <summary>
    /// Generates the source code for a migration, including a custom header block.
    /// </summary>
    /// <param name="p_MigrationNamespace">
    /// A nullable <see cref="String"/> instance representing the namespace of the migration class.
    /// </param>
    /// <param name="p_MigrationName">
    /// A <see cref="String"/> instance representing the name of the migration.
    /// </param>
    /// <param name="p_Operations">
    /// An <see cref="IReadOnlyList{MigrationOperation}"/> structure containing the operations to apply in the migration.
    /// </param>
    /// <param name="p_RollbackOperations">
    /// An <see cref="IReadOnlyList{MigrationOperation}"/> structure containing the operations to apply when rolling back the migration.
    /// </param>
    /// <returns>
    /// A <see cref="String"/> instance containing the full migration source code with header.
    /// </returns>
    public override String GenerateMigration(
        String? p_MigrationNamespace
    ,   String p_MigrationName
    ,   IReadOnlyList<MigrationOperation> p_Operations
    ,   IReadOnlyList<MigrationOperation> p_RollbackOperations) {

        var l_Header = GetHeader("Migration", p_MigrationName);
        var l_Code = base.GenerateMigration(p_MigrationNamespace, p_MigrationName, p_Operations, p_RollbackOperations);
        return l_Header + l_Code;
    }

    /// <summary>
    /// Generates the source code for a model snapshot, including a custom header block.
    /// </summary>
    /// <param name="p_ModelSnapshotNamespace">
    /// A nullable <see cref="String"/> instance representing the namespace of the snapshot class.
    /// </param>
    /// <param name="p_ContextType">
    /// A <see cref="Type"/> instance representing the DbContext type being snapshotted.
    /// </param>
    /// <param name="p_ModelSnapshotName">
    /// A <see cref="String"/> instance representing the name of the snapshot class.
    /// </param>
    /// <param name="p_Model">
    /// An <see cref="IModel"/> instance representing the EF Core model to serialize.
    /// </param>
    /// <returns>
    /// A <see cref="String"/> instance containing the full snapshot source code with header.
    /// </returns>
    public override String GenerateSnapshot(
        String? p_ModelSnapshotNamespace
    ,   Type p_ContextType
    ,   String p_ModelSnapshotName
    ,   IModel p_Model) {

        var l_Header = DatastoreMigrationsGenerator.GetHeader("Snapshot");
        var l_Code = base.GenerateSnapshot(p_ModelSnapshotNamespace, p_ContextType, p_ModelSnapshotName, p_Model);
        return l_Header + l_Code;
    }

    #endregion Public Override Methods

    #region Private Static Methods

    /// <summary>
    /// Generates a standardized header block for migration or snapshot files.
    /// </summary>
    /// <param name="p_FileType">
    /// A <see cref="String"/> instance representing the type of file being generated: "Migration" or "Snapshot".
    /// </param>
    /// <param name="p_MigrationName">
    /// A nullable <see cref="String"/> instance representing the name of the migration, if applicable.exit
    /// </param>
    /// <returns>
    /// A <see cref="String"/> instance containing the formatted header block.
    /// </returns>
    private static String GetHeader(
        String p_FileType
    ,   String? p_MigrationName = null) {
        
        var l_Timestamp = DateTime.Now.ToString("dd/MM/yyyy");
        
        return String.Join(
            Environment.NewLine
        ,   [
                "// <auto-generated>",
                "",
                "/*",
                "Everyware - Copyright © 2025 by CLS",
                "",
                "********************************************************************************************************************************************",
                "****",
                $"**** Project:           Elseware",
                $"****",
                $"**** Module:            {p_MigrationName} - {p_FileType}",
                $"****",
                $"**** Version:           2025.1.0.0001",
                $"****",
                $"**** Created By:        Cristiano Luelli",
                $"**** Created On:        {l_Timestamp}",
                $"****",
                $"**** Last Changed By:   Cristiano Luelli",
                $"**** Last Changed On:   {l_Timestamp}",
                "****",
                "********************************************************************************************************************************************",
                "",
                "Everyware - Copyright © 2025 by CLS",
                "*/",
                "",
                ""]);
    }

    #endregion Private Static Methods
}
