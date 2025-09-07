
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            IJwtService.cs
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        25/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   27/08/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

#region Usings

using Elseware.Domain;
using Noware.Dto.Responses;

#endregion Usings

namespace Elseware.Application.Interfaces;

/// <summary>
/// Provides functionality to generate JSON Web Tokens (JWT) for authenticated user accounts.
/// </summary>
public interface IJwtService {

    #region Methods

    /// <summary>
    /// Asynchronously generates a JWT token for the specified authenticated user account.
    /// </summary>
    /// <param name="p_Account">
    /// An <see cref="Account"/> instance representing the authenticated user for whom the token is to be generated.
    /// </param>
    /// <returns>
    /// A <see cref="Task{JwtResult}"/> containing the generated token and its expiration timestamp.
    /// </returns>
    Task<JwtResult> GenerateTokenAsync(Account p_Account);

    #endregion Methods
}
