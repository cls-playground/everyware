
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            ConfigurationHealthCheck
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

using Elseware.Shared.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

#endregion Usings

namespace Elseware.Diagnostics;

/// <summary>
/// Represents a health check that validates the configuration of the application.
/// </summary>
public class ConfigurationHealthCheck : IHealthCheck {

    #region Public Methods

    /// <summary>
    /// Performs a health check to determine whether the configuration of the application is valid.
    /// </summary>
    /// <param name="p_Context">
    /// A <see cref="HealthCheckContext"/> value containing the context in which the health check is being executed.
    /// </param>
    /// <param name="p_CancellationToken">
    /// A <see cref="CancellationToken"/> value containing a token that can be used to cancel the health check operation.
    /// </param>
    /// <returns>
    /// A <see cref="HealthCheckResult"/> value containing the result of the check (healthy or unhealthy).
    /// </returns>
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext p_Context
    ,   CancellationToken p_CancellationToken = default) {

        try {

            new ConfigurationBuilder().LoadConfiguration();
            return Task.FromResult(HealthCheckResult.Healthy("Configuration is valid."));
        }
        catch (Exception l_Exception) {

            return Task.FromResult(HealthCheckResult.Unhealthy(l_Exception.Message));
        }
    }

    #endregion Public Methods
}
