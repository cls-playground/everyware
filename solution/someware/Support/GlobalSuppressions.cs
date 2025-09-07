
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Someware
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

// Suppression of IDE0090 for explicit type construction in JwtAuthenticationStateProvider.
[assembly: SuppressMessage("Style", "IDE0090:Use 'new(...)'",

    Scope           = "member", 
    Target          = "~M:Someware.Authentication.JwtAuthenticationStateProvider.GetUnauthenticatedState~Microsoft.AspNetCore.Components.Authorization.AuthenticationState",
    Justification   = "Explicit type construction preferred for readability and traceability")]
