
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            AccountClaim.cs
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        23/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   25/08/2025
****
********************************************************************************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

#region Usings

using Microsoft.AspNetCore.Identity;

#endregion Usings

namespace Elseware.Domain;

/// <summary>
/// Represents a claim assigned to a user within the application.
/// </summary>
/// <remarks>
/// It is used to store user-specific information used for authentication and authorization
/// </remarks>
public class AccountClaim : IdentityUserClaim<Guid> {}
