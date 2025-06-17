using Microsoft.Extensions.DependencyInjection;

namespace ExternalUserService.CompositionRoot;

public static class CompositionRoot
{
    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddHttpClient("reqresClient", client =>
        {
            client.BaseAddress = new Uri("https://reqres.in/api/");
        });

        return services.BuildServiceProvider();
    }
}
