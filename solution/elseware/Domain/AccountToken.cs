
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            AccountToken.cs
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
/// Represents an authentication token associated with a user within the application.
/// </summary>
/// <remarks>
/// It is used to to store external authentication data or application-specific credentials, such as refresh tokens or access tokens from third-party providers.
/// </remarks>
public class AccountToken : IdentityUserToken<Guid> {}
