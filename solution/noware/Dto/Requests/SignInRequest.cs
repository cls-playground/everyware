
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           NoWare
****
**** Module:            SignInRequest
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        25/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   30/08/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

#region Usings

using System.ComponentModel.DataAnnotations;

#endregion Usings

namespace Noware.Dto.Requests;

/// <summary>
/// Represents the data required to authenticate a user during the sign-in process.
/// </summary>
public class SignInRequest {

    #region Public Properties

    /// <summary>
    /// Gets or sets the email address of a user used for authentication.
    /// </summary>
    /// <remarks>
    /// This field is required and must be a valid email format.
    /// </remarks>
    [Required]
    [EmailAddress]
    [Display(Name = "Email address")]
    public String Email { get; set; } = String.Empty;

    /// <summary>
    /// Gets or sets the password of a user used for authentication.
    /// </summary>
    /// <remarks>
    /// This field is required and must be between 6 and 50 characters long.
    /// </remarks>
    [Required]
    [DataType(DataType.Password)]
    [StringLength(50, ErrorMessage = "Password must be between {2} e {1} characters long", MinimumLength = 6)]
    public String Password { get; set; } = String.Empty;

    #endregion Public Properties
}
