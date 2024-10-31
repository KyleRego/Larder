using Microsoft.Extensions.Configuration;

namespace Larder.UITests;

public static class ConfigurationLoader
{
    public static IConfigurationRoot LoadConfiguration()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("settings.json", optional: false, reloadOnChange: true)
            .Build();
    }
}
