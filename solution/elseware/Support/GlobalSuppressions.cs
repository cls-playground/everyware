
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
**** Created On:        31/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   31/08/2025
****
**** Suppresses style warnings that conflict with CLS readability standards.
**** Each suppression is scoped to the affected member and justified explicitly.
**** 
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

using System.Diagnostics.CodeAnalysis;

// Suppressions for EF Core internal API usage in DatastoreHistoryRepository.
[assembly: SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", 
    
    Scope           = "type", 
    Target          = "~T:Elseware.Infrastructure.Persistence.Design.DatastoreHistoryRepository",
    Justification   =
        "This customization is required to override EF Core's default migration behavior."
    +   "The internal API is used intentionally and safely within a controlled context.")]
