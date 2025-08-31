
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Someware
****
**** Module:            RedirectService.cs
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        31/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   31/08/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

#region Usings

using Microsoft.AspNetCore.Components;

#endregion Usings

namespace Someware.Services.Navigation;

/// <summary>
/// Provides centralized logic for redirecting users after authentication-related actions.
/// </summary>
/// <remarks>
/// This service validates and sanitizes return URLs, applies fallback destinations,
/// and ensures safe navigation within the application context.
/// </remarks>
public class RedirectService(
    NavigationManager navigationManager) {

    #region Private Readonly Fields

    /// <summary>
    /// A <see cref="NavigationManager"/> instance used to perform client-side navigation.
    /// </summary>
    private readonly NavigationManager navigationManager = navigationManager;

    #endregion Private Readonly Fields

    #region Public Methods

    /// <summary>
    /// Redirects the user to the specified return URL or a fallback destination.
    /// </summary>
    /// <param name="p_ReturnUrl">
    /// A nullable <see cref="String"/> instance representing the return URL passed via query string.
    /// </param>
    /// <param name="p_FallbackUrl">
    /// A <see cref="String"/> instance representing the fallback URL to use if the return URL is invalid or missing.
    /// </param>
    public void RedirectTo(
        String? p_ReturnUrl
    ,   String p_FallbackUrl = "/") {

        var l_SafeUrl = this.SanitizeReturnUrl(p_ReturnUrl);
        this.navigationManager.NavigateTo(l_SafeUrl ?? p_FallbackUrl);
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Validates and sanitizes the return URL to ensure it is safe and relative.
    /// </summary>
    /// <param name="p_Url">
    /// A nullable <see cref="String"/> instance representing the raw return URL.
    /// </param>
    /// <returns>
    /// A <see cref="String"/> instance containing the sanitized relative URL, or <c>null</c> if invalid.
    /// </returns>
    private String? SanitizeReturnUrl(
        String? p_Url) {

        if (String.IsNullOrWhiteSpace(p_Url)) {
            return null;
        }

        if (p_Url.StartsWith('/') is false) {
            p_Url = $"/{p_Url}";
        }

        if (Uri.TryCreate(p_Url, UriKind.RelativeOrAbsolute, out var p_CandidateUri) is false) {
            return null;
        }

        var p_BaseUri = new Uri(this.navigationManager.BaseUri);
        var p_AbsoluteUri = 
            p_CandidateUri.IsAbsoluteUri
                ?   p_CandidateUri
                :   new Uri(p_BaseUri, p_CandidateUri);

        return 
            p_AbsoluteUri.Scheme != p_BaseUri.Scheme || p_AbsoluteUri.Host != p_BaseUri.Host || p_AbsoluteUri.Port != p_BaseUri.Port
                ?   null
                :   p_AbsoluteUri.PathAndQuery;
    }

    #endregion Private Methods
}
