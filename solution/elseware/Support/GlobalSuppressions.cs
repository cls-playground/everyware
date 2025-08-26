
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            GlobalSuppressions.cs
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

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("EF1001", "EF1001:Internal EF Core API usage",

    Scope           = "type",
    Target          = "~T:Elseware.Infrastructure.Persistence.DatastoreMigration",
    Justification   = 
        "This class intentionally extends SqlServerHistoryRepository to customize the EF Core migration history table." +
        "The use of internal EF Core APIs is required for column renaming and schema control.")]

[assembly: SuppressMessage("Style", "IDE2001:Embedded statements must be on their own line",
    
    Scope           = "module",
    Justification   = "Brace placement intentionally kept on the same line for consistency with project formatting style.")]

[assembly: SuppressMessage("Style", "IDE0046:Convert to conditional expression",

    Scope           = "module",
    Justification   = "Explicit branching improves readability and maintains clarity in conditional logic.")]
