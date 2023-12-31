using Microsoft.Extensions.Configuration;

namespace Notes.Tests.Acceptance.Infrastructure;

public class AppSettings
{
    private const string AppSettingsFileName = "appsettings.test.json";
    private const string BaseApiUrlSettingName = "BaseApiUrl";
    private const string TokenPrefixSettingName = "TokenPrefix";
    private static IConfiguration _config;

    static AppSettings()
    {
        string? baseApiUrl = Environment.GetEnvironmentVariable(BaseApiUrlSettingName);

        _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(AppSettingsFileName)
            .Build();

        _config[BaseApiUrlSettingName] = !string.IsNullOrEmpty(baseApiUrl) ? baseApiUrl : _config[BaseApiUrlSettingName];
    }

    public static string BaseApiUrl => _config[BaseApiUrlSettingName];
    public static string TokenPrefix => _config[TokenPrefixSettingName];
}
