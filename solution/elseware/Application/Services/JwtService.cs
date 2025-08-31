
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            JwtService.cs
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

using Elseware.Application.Interfaces;
using Elseware.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Noware.Dto.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

#endregion Usings

namespace Elseware.Application.Services;

/// <summary>
/// Provides functionality for generating JSON Web Tokens (JWT) for authenticated user accounts.
/// </summary>
public class JwtService(
    UserManager<Account> p_UserManager
,   IConfiguration p_Configuration) : IJwtService {

    #region Private Readonly Fields

    /// <summary>
    /// A <see cref="UserManager{Account}"/> instance providing support for user-related operations such as retrieving roles and identity
    /// information.
    /// </summary>
    private readonly UserManager<Account> userManager = p_UserManager;

    /// <summary>
    /// An <see cref="IConfiguration"/> instance providing access to application configuration settings, including JWT parameters.
    /// </summary>
    private readonly IConfiguration configuration = p_Configuration;

    #endregion Private Readonly Fields

    #region Public Async Methods

    /// <summary>
    /// Asynchronously generates a JWT token for the specified authenticated user account.
    /// </summary>
    /// <param name="p_Account">
    /// An <see cref="Account"/> instance representing the authenticated user for whom the token is to be generated.
    /// </param>
    /// <returns>
    /// A <see cref="Task{JwtResult}"/> containing the generated token and its expiration timestamp.
    /// </returns>
    public async Task<JwtResult> GenerateTokenAsync(
        Account p_Account) {

        if (String.IsNullOrWhiteSpace(p_Account.UserName)) {
            return new JwtResult();
        }

        var l_SecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
        if (String.IsNullOrWhiteSpace(l_SecretKey)) {
            return new JwtResult();
        }

        var l_SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(l_SecretKey));
        var l_Credentials = new SigningCredentials(l_SecurityKey, SecurityAlgorithms.HmacSha256);
        var l_RoleNames = await userManager.GetRolesAsync(p_Account);

        var l_Claims = new List<Claim> {
            new(ClaimTypes.NameIdentifier, p_Account.Id.ToString()),
            new(ClaimTypes.Name, p_Account.UserName),
            new(ClaimTypes.Email, p_Account.UserName),
            new(JwtRegisteredClaimNames.Sub, p_Account.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }.Union(l_RoleNames.Select(role => new Claim(ClaimTypes.Role, role)));

        var l_TokenLifetimeString = configuration["Jwt:TokenLifetimeMinutes"];
        var l_TokenLifetime = Int32.TryParse(l_TokenLifetimeString, out var l_Minutes) ? l_Minutes : 10;

        var l_ExpiresAt = DateTime.UtcNow.AddMinutes(l_TokenLifetime);
        var l_JwtToken = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: l_Claims,
            notBefore: DateTime.UtcNow,
            expires: l_ExpiresAt,
            signingCredentials: l_Credentials
        );

        var l_Token = new JwtSecurityTokenHandler().WriteToken(l_JwtToken);
        return new JwtResult { Token = l_Token, ExpiresAt = l_ExpiresAt };
    }

    #endregion Public Async Methods
}
