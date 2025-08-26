
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            AccountRole.cs
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        23/08/2025
****
**** Last Change By:    Cristiano Luelli
**** Last Change On:    25/08/2025
****
********************************************************************************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

#region Usings

using Microsoft.AspNetCore.Identity;

#endregion Usings

namespace Elseware.Domain;

/// <summary>
/// Represents the association between a user and a role within the application.
/// </summary>
/// <remarks>
/// It is used to store role assignments for users.
/// </remarks>
public class AccountRole : IdentityUserRole<Guid> {}
