public static class ConfigurationExtensions
{
    public static T GetValue<T>(this IConfiguration? configuration, string? key)
    {
        // Check if the key exists in the environment variables
        var value = Environment.GetEnvironmentVariable(key);
        if (!string.IsNullOrEmpty(value))
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
        // Get the value from the appsettings.json file
        //c configuration.GetValue<T>(key);
        if (configuration == null)
        {
            configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Staging"}.json", true)
            .Build();
        }
        return ConfigurationBinder.GetValue(configuration, key, default(T));
    }
}