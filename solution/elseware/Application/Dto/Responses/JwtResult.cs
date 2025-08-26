
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            JwtResult.cs
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        25/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   26/08/205
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

namespace Elseware.Application.Dto.Responses;

/// <summary>
/// Represents the result of a JWT token generation process, including the token string and its expiration timestamp.
/// </summary>
public class JwtResult {

    #region Public Properties

    /// <summary>
    /// Gets or sets the encoded JWT token string issued for the authenticated user.
    /// </summary>
    public String Token { get; set; } = String.Empty;

    /// <summary>
    /// Gets or sets the UTC timestamp indicating when the token will expire.
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    #endregion Public Properties
}
