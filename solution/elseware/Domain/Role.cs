
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            Role.cs
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
/// Represents a role within the application.
/// </summary>
/// <remarks>
/// It is used to group users and assign common permissions for role-based authorization.
/// </remarks>
public class Role : IdentityRole<Guid> {}
