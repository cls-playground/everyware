
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           NoWare
****
**** Module:            SignUpRequest
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
/// Represents the data required to register a new user account.
/// </summary>
public class SignUpRequest {

    #region Public Properties

    /// <summary>
    /// Gets or sets a value used for password confirmation.
    /// </summary>
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public String ConfirmPassword { get; set; } = String.Empty;

    /// <summary>
    /// Gets or sets the email address of the user used for account registration.
    /// </summary>
    /// <remarks>
    /// This field is required and must be a valid email format.
    /// </remarks>
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public String Email { get; set; } = String.Empty;

    /// <summary>
    /// Gets or sets the password of the user used for account registration.
    /// </summary>
    /// <remarks>
    /// This field is required and must be between 6 and 50 characters long.
    /// </remarks>
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public String Password { get; set; } = String.Empty;

    #endregion Public Properties
}
