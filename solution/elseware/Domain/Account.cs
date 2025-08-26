
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            Account.cs
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        23/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   25/08/2025
****
********************************************************************************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

#region Usings

using Microsoft.AspNetCore.Identity;

#endregion Usings

namespace Elseware.Domain;

/// <summary>
/// Represents a user within the application.
/// </summary>
public class Account : IdentityUser<Guid> {

    #region Public Properties

    /// <summary>
    /// Gets or sets the date of birth of the user.
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public String? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public String? LastName { get; set; }

    #endregion Public Properties
}
