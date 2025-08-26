
/*
Everyware - Copyright © 2025 by CLS

********************************************************************************************************************************************
****
**** Project:           Elseware
****
**** Module:            ConfigurationBuilderExtension.cs
****
**** Version:           2025.1.0.0001
****
**** Created By:        Cristiano Luelli
**** Created On:        25/08/2025
****
**** Last Changed By:   Cristiano Luelli
**** Last Changed On:   25/08/2025
****
********************************************************************************************************************************************

Everyware - Copyright © 2025 by CLS
*/

#region Usings

using System.Text.Json;

#endregion Usings

namespace Elseware.Shared.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IConfigurationBuilder"/> to load configuration files.
/// </summary>
public static class ConfigurationBuilderExtensions {

    #region Public Static Methods

    /// <summary>
    /// Loads configuration files, including environment-specific overrides, into the provided configuration builder.
    /// </summary>
    /// <param name="p_ConfigurationBuilder">
    /// The <see cref="IConfigurationBuilder"/> instance to extend.
    /// </param>
    /// <param name="p_BasePath">
    /// A <see cref="String"/> instance representing the base path where configuration files are located (defaults to <c>Settings</c>).
    /// </param>
    /// <returns>
    /// An <see cref="IConfigurationRoot"/> instance containing fully built merged configuration values.
    /// </returns>
    public static IConfigurationRoot LoadConfiguration(
        this IConfigurationBuilder p_ConfigurationBuilder
    ,   String p_BasePath = "Settings") {

        var l_EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        CheckConfigurationFiles(l_EnvironmentName);
        p_ConfigurationBuilder
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), p_BasePath))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{l_EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        return p_ConfigurationBuilder.Build();
    }

    #endregion Public Static Methods

    #region Private Static Methods

    /// <summary>
    /// Checks whether configuration files exist.
    /// </summary>
    /// <param name="p_EnvironmentName">
    /// A <see cref="String"/> instance representing the name of the current environment.
    /// </param>
    /// <exception cref="FileNotFoundException">
    /// Thrown when one or both of the required configuration files are missing from the expected <c>Settings</c> directory.
    /// Specifically, this applies to <c>appsettings.json</c> and <c>appsettings.{Environment}.json</c>.
    /// </exception>
    private static void CheckConfigurationFiles(
        String p_EnvironmentName) {

        var l_BasePath = Directory.GetCurrentDirectory();
        var l_MainConfigurationFilePath = Path.Combine(l_BasePath, "Settings", "appsettings.json");
        var l_EnvironmentConfigurationFilePath = Path.Combine(l_BasePath, "Settings", $"appsettings.{p_EnvironmentName}.json");

        if (File.Exists(l_MainConfigurationFilePath) is false) {
            throw new FileNotFoundException($"The configuration file [{l_MainConfigurationFilePath}] is missing.");
        }

        if (File.Exists(l_EnvironmentConfigurationFilePath) is false) {
            throw new FileNotFoundException($"The configuration file [{l_EnvironmentConfigurationFilePath}] is missing.");
        }

        ////// KEEP:    Right now the validation of the main configuration file is useless, since cors and connectionstring are environmental 
        //////          attributes...
        ////// ValidateConfigurationFile(l_MainConfigurationFilePath);
        ValidateConfigurationFile(l_EnvironmentConfigurationFilePath);
    }

    /// <summary>
    /// Validates the structure and content of a JSON configuration file by inspecting its root element.
    /// </summary>
    /// <param name="p_ConfigurationFilePath">
    /// A <see cref="String"/> instance representing the full path to the configuration file to be validated.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the specified configuration file contains invalid JSON syntax and cannot be parsed.
    /// </exception>
    private static void ValidateConfigurationFile(
        String p_ConfigurationFilePath) {

        try {
            var l_JsonConfiguration = File.ReadAllText(p_ConfigurationFilePath);
            using var l_JsonDocument = JsonDocument.Parse(l_JsonConfiguration);
            var l_RootElement = l_JsonDocument.RootElement;

            ValidateConfigurationFileCors(p_ConfigurationFilePath, l_RootElement);
            ValidateConfigurationFileDatastore(p_ConfigurationFilePath, l_RootElement);
            ValidateConfigurationFileJwt(p_ConfigurationFilePath, l_RootElement);
        }
        catch (JsonException l_JsonException) {
            throw new InvalidOperationException(
                $"Invalid JSON syntax in [{p_ConfigurationFilePath}]: {l_JsonException.Message}");
        }
    }

    /// <summary>
    /// Validates the presence and structure of the <c>Cors</c> section within a JSON configuration file.
    /// </summary>
    /// <param name="p_ConfigurationFilePath">
    /// A <see cref="String"/> instance representing the full path to the configuration file being validated.
    /// </param>
    /// <param name="p_RootElement">
    /// A <see cref="JsonElement"/> instance representing the root element of the parsed JSON document.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the <c>Cors</c> section or its <c>AllowedOrigins</c> key is missing, or when <c>AllowedOrigins</c> is not a valid JSON
    /// array.
    /// </exception>
    private static void ValidateConfigurationFileCors(
        String p_ConfigurationFilePath
    ,   JsonElement p_RootElement) {

        if (p_RootElement.TryGetProperty("Cors", out var l_CorsSection) is false) {
            throw new InvalidOperationException($"Missing 'Cors' section in [{p_ConfigurationFilePath}]");
        }

        if (l_CorsSection.TryGetProperty("AllowedOrigins", out var l_AllowedOriginsKey) is false) {
            throw new InvalidOperationException($"Missing 'Cors:AllowedOrigins' key in [{p_ConfigurationFilePath}]");
        }
            
        if (l_AllowedOriginsKey.ValueKind is not JsonValueKind.Array) {
            throw new InvalidOperationException(
                $"Invalid 'Cors:AllowedOrigins' parameter in [{p_ConfigurationFilePath}]");
        }
    }

    /// <summary>
    /// Validates the presence and structure of the <c>Datastore</c> section within a JSON configuration file.
    /// </summary>
    /// <param name="p_ConfigurationFilePath">
    /// A <see cref="String"/> instance representing the full path to the configuration file being validated.
    /// </param>
    /// <param name="p_RootElement">
    /// A <see cref="JsonElement"/> instance representing the root element of the parsed JSON document.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the <c>Datastore</c> section is missing, the <c>ConnectionString</c> key is not present,
    /// or the <c>ConnectionString</c> value is not a valid JSON string.
    /// </exception>
    private static void ValidateConfigurationFileDatastore(
        String p_ConfigurationFilePath
    ,   JsonElement p_RootElement) {

        if (p_RootElement.TryGetProperty("Datastore", out var l_DatastoreSection) is false) {
            throw new InvalidOperationException($"Missing 'Datastore' section in [{p_ConfigurationFilePath}]");
        }

        if (l_DatastoreSection.TryGetProperty("ConnectionString", out var l_ConnectionStringKey) is false) {
            throw new InvalidOperationException(
                $"Missing 'Datastore:ConnectionString' key in [{p_ConfigurationFilePath}]");
        }

        if (l_ConnectionStringKey.ValueKind is not JsonValueKind.String) {
            throw new InvalidOperationException(
                $"Invalid 'Datastore:ConnectionString' parameter in [{p_ConfigurationFilePath}]");
        }
    }

    /// <summary>
    /// Validates the presence and structure of the <c>Jwt</c> section within a JSON configuration file.
    /// </summary>
    /// <param name="p_ConfigurationFilePath">
    /// A <see cref="String"/> instance representing the full path to the configuration file being validated.
    /// </param>
    /// <param name="p_RootElement">
    /// A <see cref="JsonElement"/> instance representing the root element of the parsed JSON document.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the <c>Jwt</c> section is missing, or when any of the required keys
    /// (<c>Issuer</c>, <c>Audience</c>, <c>AccessTokenExpirationMinutes</c>, <c>RefreshTokenExpirationMinutes</c>)
    /// are not present or contain invalid value types.
    /// </exception>
    private static void ValidateConfigurationFileJwt(
        String p_ConfigurationFilePath
    ,   JsonElement p_RootElement) {

        if (p_RootElement.TryGetProperty("Jwt", out var l_JwtSection) is false) {
            throw new InvalidOperationException($"Missing 'Jwt' section in [{p_ConfigurationFilePath}]");
        }

        if (l_JwtSection.TryGetProperty("Issuer", out var l_IssuerKey) is false) {
            throw new InvalidOperationException($"Missing 'Jwt:Issuer' key in [{p_ConfigurationFilePath}]");
        }

        if (l_IssuerKey.ValueKind is not JsonValueKind.String) {
            throw new InvalidOperationException($"Invalid 'Jwt:Issuer' parameter in [{p_ConfigurationFilePath}]");
        }

        if (l_JwtSection.TryGetProperty("Audience", out var l_AudienceKey) is false) {
            throw new InvalidOperationException($"Missing 'Jwt:Audience' key in [{p_ConfigurationFilePath}]");
        }

        if (l_AudienceKey.ValueKind is not JsonValueKind.String) {
            throw new InvalidOperationException($"Invalid 'Jwt:Audience' parameter in [{p_ConfigurationFilePath}]");
        }

        if (l_JwtSection.TryGetProperty("AccessTokenExpirationMinutes", out var l_AccessTokenExpirationMinutesKey) is false) {
            throw new InvalidOperationException(
                $"Missing 'Jwt:AccessTokenExpirationMinutes' key in [{p_ConfigurationFilePath}]");
        }

        if (l_AccessTokenExpirationMinutesKey.ValueKind is not JsonValueKind.Number) {
            throw new InvalidOperationException(
                $"Invalid 'Jwt:AccessTokenExpirationMinutes' parameter in [{p_ConfigurationFilePath}]");
        }

        if (l_JwtSection.TryGetProperty("RefreshTokenExpirationMinutes", out var l_RefreshTokenExpirationMinutesKey) is false) {
            throw new InvalidOperationException(
                $"Missing 'Jwt:RefreshTokenExpirationMinutes' key in [{p_ConfigurationFilePath}]");
        }

        if (l_RefreshTokenExpirationMinutesKey.ValueKind is not JsonValueKind.Number) {
            throw new InvalidOperationException(
                $"Invalid 'Jwt:RefreshTokenExpirationMinutes' parameter in [{p_ConfigurationFilePath}]");
        }
    }

    #endregion Private Static Methods
}
