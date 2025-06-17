using Microsoft.Extensions.DependencyInjection;

namespace ExternalUserService.CompositionRoot;

public static class CompositionRoot
{
    public static ServiceCollection ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddHttpClient(ExternalUserHttpClient.ClientId, client =>
        {
            client.BaseAddress = new Uri("https://reqres.in/api/");
        });

        return services;
    }
}
