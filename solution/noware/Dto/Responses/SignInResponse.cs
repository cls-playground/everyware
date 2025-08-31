
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           NoWare
****
**** Module:            SignInResponse.cs
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        25/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   31/08/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

namespace Noware.Dto.Responses;

/// <summary>
/// Represents the response returned after a successful user sign-in, including generated JWT token, user identifier and assigned roles.
/// </summary>
public class SignInResponse {

    #region Public Properties

    /// <summary>
    /// Gets or sets the JWT token and its expiration details for the authenticated user.
    /// </summary>
    public JwtResult Jwt { get; set; } = new JwtResult();

    /// <summary>
    /// Gets or sets the collection of role names assigned to the user.
    /// </summary>
    public IEnumerable<String> Roles { get; set; } = [];

    /// <summary>
    /// Gets or sets the unique identifier of the signed-in user.
    /// </summary>
    public String UserId { get; set; } = String.Empty;

    #endregion Public Properties
}
