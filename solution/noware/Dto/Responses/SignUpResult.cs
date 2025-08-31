
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           NoWare
****
**** Module:            SignUpResult.cs
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        30/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   31/08/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

namespace Noware.Dto.Responses;

/// <summary>
/// Represents the outcome of a user registration attempt.
/// </summary>
public class SignUpResult {

    #region Public Properties

    /// <summary>
    /// Gets or sets a collection of error messages returned from the backend.
    /// </summary>
    public IEnumerable<String>? Errors { get; set; }

    /// <summary>
    /// Gets or sets a human-readable message describing the result.
    /// </summary>
    public String? Message { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the registration was successful.
    /// </summary>
    public Boolean Success { get; set; }

    #endregion Public Properties
}
