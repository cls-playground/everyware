
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Someware
****
**** Module:            JwtAuthenticationStateProvider.cs
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

using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

#endregion Usings

namespace Someware.Authentication;

/// <summary>
/// Provides the authentication state for the Blazor WebAssembly application by evaluating a JWT token stored in the local storage of the 
/// browser.
/// </summary>
/// <param name="p_LocalStorageService">
/// An <see cref="ILocalStorageService"/> instance providing access to the local storage of the browser.
/// </param>
public class JwtAuthenticationStateProvider(
    ILocalStorageService p_LocalStorageService) : AuthenticationStateProvider {

    #region Private Readonly Fields

    /// <summary>
    /// An <see cref="ILocalStorageService"/> instance providing access to the local storage of the browser.
    /// </summary>    
    private readonly ILocalStorageService localStorageService = p_LocalStorageService;

    #endregion Private Readonly Fields

    #region Public Override Async Methods

    /// <summary>
    /// Retrieves the current authentication state by evaluating the stored JWT token.
    /// </summary>
    /// <returns>
    /// A <see cref="Task{AuthenticationState}"/> representing the asynchronous operation that returns the authentication state of the
    /// current user.
    /// </returns>
    /// <remarks>
    /// If no valid token is found or the token is expired, an unauthenticated <see cref="ClaimsPrincipal"/> is returned.
    /// </remarks>    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync() {

        var l_Token = await this.localStorageService.GetItemAsync<String>("authToken");

        if (String.IsNullOrWhiteSpace(l_Token)) {
            return GetUnauthenticatedState();
        }

        var l_TokenHandler = new JwtSecurityTokenHandler();

        try {
            var l_JwtToken = l_TokenHandler.ReadJwtToken(l_Token);
            var l_Expiration = l_JwtToken.ValidTo;
            if (l_Expiration < DateTime.UtcNow.AddSeconds(-30)) {
                return GetUnauthenticatedState();
            }

            var l_Claims = l_JwtToken.Claims;
            var l_Identity = new ClaimsIdentity(l_Claims, "jwt");
            var l_User = new ClaimsPrincipal(l_Identity);
            return new AuthenticationState(l_User);
        }
        catch {
            return GetUnauthenticatedState();
        }
    }

    #endregion Public Override Async Methods

    #region Public Async Methods

    /// <summary>
    /// Marks the current user as authenticated by storing the provided JWT token and notifying the application of the updated 
    /// authentication state.
    /// </summary>
    /// <param name="p_Token">
    /// A <see cref="String"/> instance representing the valid JWT token of the authenticated user.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> instance representing the asynchronous operation.
    /// </returns>
    public async Task MarkUserAsAuthenticated(
        String p_Token) {

        await this.localStorageService.SetItemAsync("authToken", p_Token);
        var l_AuthenticationState = await this.GetAuthenticationStateAsync();
        this.NotifyAuthenticationStateChanged(Task.FromResult(l_AuthenticationState));
    }

    /// <summary>
    /// Signs out the current user by removing the JWT token and notifying the application of the updated authentication state.
    /// </summary>
    /// <returns>
    /// A <see cref="Task"/> representing the asynchronous operation.
    /// </returns>
    public async Task SignOut() {

        await this.localStorageService.RemoveItemAsync("authToken");
        this.NotifyAuthenticationStateChanged(Task.FromResult(GetUnauthenticatedState()));
    }

    #endregion Public Async Methods

    #region Private Static Methods

    /// <summary>
    /// Creates an unauthenticated <see cref="AuthenticationState"/> with an empty <see cref="ClaimsPrincipal"/>.
    /// </summary>
    /// <returns>
    /// An <see cref="AuthenticationState"/> instance representing the unauthenticated state of the user.
    /// </returns>
    private static AuthenticationState GetUnauthenticatedState() 
    
    =>  new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

    #endregion Private Static Methods
}
