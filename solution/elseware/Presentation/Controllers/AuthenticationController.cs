
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            AuthenticationController
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        25/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   26/08/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

#region Usings

using Elseware.Application.Dto.Requests;
using Elseware.Application.Dto.Responses;
using Elseware.Application.Interfaces;
using Elseware.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

#endregion Usings

namespace Elseware.Presentation.Controllers;

/// <summary>
/// Handles user authentication operations such as sign-in and sign-up.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(
    SignInManager<Account> p_SignInManager
,   UserManager<Account> p_UserManager
,   IConfiguration p_Configuration
,   IJwtService p_JwtService) : ControllerBase {

    #region Private Fields

    private readonly SignInManager<Account> m_SignInManager = p_SignInManager;
    private readonly UserManager<Account> m_UserManager = p_UserManager;
    private readonly IConfiguration m_Configuration = p_Configuration;
    private readonly IJwtService m_JwtService = p_JwtService;

    #endregion Private Fields

    #region Public Async Methods

    /// <summary>
    /// Authenticates a user based on provided credentials and returns a JWT token if successful.
    /// </summary>
    /// <param name="p_SignIn">
    /// A <see cref="SignInRequest"/> instance representing user credentials.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> representing the <see cref="SignInResponse"/> containing the JWT token.
    /// </returns>    
    [HttpPost]
    [Route("signin")]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn(
        [FromBody] SignInRequest p_SignIn) {

        if (this.ModelState.IsValid is false) {
            return this.BadRequest(new { Message = "Invalid request data", Errors = this.ModelState });
        }

        var l_SignInResult = await m_SignInManager.PasswordSignInAsync(p_SignIn.Email, p_SignIn.Password, false, false);
        if (l_SignInResult.Succeeded == true) {
            var l_SignedInAccount = await m_UserManager.FindByEmailAsync(p_SignIn.Email);
            if (l_SignedInAccount is null) {
                return this.Unauthorized(new { Message = "Authentication failed. Please check your credentials and try again." });
            }

            var l_JwtResult = await m_JwtService.GenerateTokenAsync(l_SignedInAccount);
            if (String.IsNullOrWhiteSpace(l_JwtResult.Token)) {
                return this.BadRequest(
                    new { 
                            Message = 
                                "Token generation failed due to missing or invalid user claims. "
                            +   "Please verify the account data and try again." });
            }

            var l_Response = new SignInResponse {
                Jwt = l_JwtResult,
                UserId = l_SignedInAccount.Id.ToString(),
                Roles = await m_UserManager.GetRolesAsync(l_SignedInAccount)
            };

            return this.Ok(l_Response);
        }

        return this.Unauthorized(p_SignIn);
    }

    /// <summary>
    /// Registers a new user account using provided credentials.
    /// </summary>
    /// <param name="p_SignUp">
    /// A <see cref="SignUpRequest"/> instance representing user credentials.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> representing the <see cref="SignUpResponse"/> containing user registration details.
    /// </returns>    
    [HttpPost]
    [AllowAnonymous]
    [Route("signup")]
    public async Task<IActionResult> SignUp(
        [FromBody] SignUpRequest p_SignUp) {

        if (this.ModelState.IsValid is false) {
            return this.BadRequest(new { Message = "Invalid request data", Errors = this.ModelState });
        }

        var l_SignUpAccount =
            new Account {
                    Email = p_SignUp.Email
                ,   UserName = p_SignUp.Email
            };

        var l_AccountResult = await m_UserManager.CreateAsync(l_SignUpAccount, p_SignUp.Password);
        if (l_AccountResult.Succeeded == true) {
            return this.StatusCode(StatusCodes.Status201Created, new { l_AccountResult.Succeeded });
        }

        var l_Errors = l_AccountResult.Errors.Select(p_Error => new { p_Error.Code, p_Error.Description });
        return this.BadRequest(new { Message = "Sign up failed", l_Errors });
    }

    #endregion Public Async Methods
}
