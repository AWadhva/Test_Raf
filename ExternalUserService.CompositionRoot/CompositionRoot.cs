using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExternalUserService.CompositionRoot;

public static class CompositionRoot
{
    public static ServiceCollection ConfigureServices()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var services = new ServiceCollection();

        services.AddSingleton<IConfiguration>(configuration);

        services.AddHttpClient(ExternalUserHttpClient.ClientId, client =>
        {
            var baseAddress = configuration["ExternalClient:BaseAddress"];

            if (string.IsNullOrEmpty(baseAddress))
                throw new InvalidOperationException("BaseAddress not configured.");

            client.BaseAddress = new Uri(baseAddress);            
        });
        
        services.AddSingleton<UserService>();

        services.AddSingleton<IExternalUserClient, ExternalUserHttpClient>();

        return services;
    }
}
