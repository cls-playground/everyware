
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Someware
****
**** Module:            AuthenticationService.cs
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        27/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   31/08/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

#region Usings

using Noware.Dto.Requests;
using Noware.Dto.Responses;
using Serilog.Core;
using Someware.Authentication;
using System.Net.Http.Headers;
using System.Net.Http.Json;

#endregion Usings

namespace Someware.Services.Authentication;

/// <summary>
/// Provides authentication operations such as sign-in, sign-up, and sign-out by interacting with the backend API and updating the authentication state.
/// </summary>
/// <param name="httpClient">
/// The HTTP client used to communicate with the backend API.
/// </param>
/// <param name="logger">
/// The logger used to record diagnostic and operational information.
/// </param>
public class AuthenticationService(
    HttpClient p_HttpClient
,   JwtAuthenticationStateProvider p_AuthenticationStateProvider
,   ILogger<AuthenticationService> p_Logger) {

    #region Private Readonly Fields

    /// <summary>
    /// A <see cref="HttpClient"/> instance used to communicate with the backend API for authentication operations.
    /// </summary>
    private readonly HttpClient httpClient = p_HttpClient;

    /// <summary>
    /// An <see cref="ILogger{AuthenticationService}"/> instance used to record diagnostic and operational information.
    /// </summary>
    private readonly ILogger<AuthenticationService> logger = p_Logger;

    /// <summary>
    /// A <see cref="JwtAuthenticationStateProvider"/> instance representing the authentication state manager used to update user identity.
    /// </summary>
    private readonly JwtAuthenticationStateProvider authenticationStateProvider = p_AuthenticationStateProvider;

    #endregion Private Readonly Fields

    #region Public Async Methods

    /// <summary>
    /// Sends user credentials to the backend API and updates the authentication state upon success.
    /// </summary>
    /// <param name="p_Request">
    /// A <see cref="SignInRequest"/> instance representing provided user credentials.
    /// </param>
    /// <returns>
    /// A <see cref="Boolean"/> structure indicating whether authentication succeeds.
    /// </returns>
    public async Task<Boolean> SignInAsync(
        SignInRequest p_Request) {

        var l_Response = await httpClient.PostAsJsonAsync("api/authentication/signin", p_Request);
        if (l_Response.IsSuccessStatusCode is false) {
            return false;
        }

        var l_Result = await l_Response.Content.ReadFromJsonAsync<SignInResponse>();
        if (l_Result is null || String.IsNullOrWhiteSpace(l_Result.Jwt.Token)) {
            return false;
        }

        await authenticationStateProvider.MarkUserAsAuthenticated(l_Result.Jwt.Token);

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", l_Result.Jwt.Token);

        return true;
    }

    /// <summary>
    /// Sends user registration data to the backend API.
    /// </summary>
    /// <param name="p_Request">
    /// A <see cref="SignUpRequest"/> instance representing user details.
    /// </param>
    /// <returns>
    /// A <see cref="Boolean"/> structure indicating whether registration succeeds.
    /// </returns>
    public async Task<SignUpResult> SignUpAsync(
        SignUpRequest p_Request) {

        // Logs the attempt
        logger.LogInformation("Attempting user registration for email {Email}", p_Request.Email);

        try {
            var l_Response = await httpClient.PostAsJsonAsync("api/authentication/signup", p_Request);

            if (l_Response.IsSuccessStatusCode) {
                logger.LogInformation("User registration succeeded for email {Email}", p_Request.Email);
                return new SignUpResult { Success = true };
            }

            var l_ErrorContent = await l_Response.Content.ReadAsStringAsync();
            logger.LogWarning("User registration failed for email {Email}. Response: {Error}", p_Request.Email, l_ErrorContent);

            return new SignUpResult {
                Success = false,
                Message = "Registration failed",
                Errors = [l_ErrorContent]
            };
        }
        catch (Exception ex) {
            logger.LogError(ex, "Exception occurred during registration for email {Email}", p_Request.Email);

            return new SignUpResult {
                Success = false,
                Message = "An unexpected error occurred during registration.",
                Errors = [ex.Message]
            };
        }
    }

    /// <summary>
    /// Signs out the current user by clearing the token and updating the authentication state.
    /// </summary>
    /// <returns>
    /// A <see cref="Task"/> instance representing the asynchronous operation.
    /// </returns>
    public async Task SignOutAsync()
    
    =>  await authenticationStateProvider.SignOut();

    #endregion Public Async Methods
}
