
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           NoWare
****
**** Module:            SignUpResponse.cs
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        29/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   31/08/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

namespace Noware.Dto.Responses;

/// <summary>
/// Represents the response returned after a successful or failed user sign-up operation.
/// </summary>
public class SignUpResponse {

    #region Public Properties

    /// <summary>
    /// Gets or sets the collection of error messages returned during sign-up failure.
    /// </summary>
    public IEnumerable<String> Errors { get; set; } = [];

    /// <summary>
    /// Gets or sets a value indicating whether the sign-up operation succeeded.
    /// </summary>
    public Boolean Success { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the newly created user account, if available.
    /// </summary>
    public String? UserId { get; set; }

    #endregion Public Properties
}
