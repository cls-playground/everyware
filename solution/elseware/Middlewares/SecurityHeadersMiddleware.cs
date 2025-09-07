
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            SecurityHeadersMiddleware.cs
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        21/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   21/08/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

namespace Elseware.Middlewares;

/// <summary>
/// Provides functionality to apply standard HTTP security headers into outgoing responses.
/// </summary>
/// <param name="p_NextMiddleware">
/// A <see cref="RequestDelegate"/> instance representing the next middleware in the HTTP request pipeline.
/// </param>
public class SecurityHeadersMiddleware(
    RequestDelegate p_NextMiddleware) {

    /// <summary>
    /// A <see cref="RequestDelegate"/> instance representing the next middleware in the HTTP request pipeline.
    /// </summary>
    private readonly RequestDelegate nextMiddleware = p_NextMiddleware;

    /// <summary>
    /// Adds security headers to the HTTP response.
    /// </summary>
    /// <param name="p_HttpContext">
    /// A <see cref="HttpContent"/> instance representing the current HTTP context.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> instance representing the asynchronous operation.
    /// </returns>
    public async Task Invoke(
        HttpContext p_HttpContext) {

        var l_ResponseHeaders = p_HttpContext.Response.Headers;

        l_ResponseHeaders.XContentTypeOptions = "nosniff";
        l_ResponseHeaders.XFrameOptions = "DENY";
        l_ResponseHeaders.XXSSProtection = "1; mode=block";
        l_ResponseHeaders.StrictTransportSecurity = "max-age=31536000; includeSubDomains";
        l_ResponseHeaders.ContentSecurityPolicy = "default-src 'self'; script-src 'self'; style-src 'self';";

        l_ResponseHeaders["Referrer-Policy"] = "no-referrer";

        await this.nextMiddleware(p_HttpContext);
    }
}
