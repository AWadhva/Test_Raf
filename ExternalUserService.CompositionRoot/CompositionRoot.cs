using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace ExternalUserService.CompositionRoot;

public static class CompositionRoot
{
    public const string BaseAddress = "ExternalClient:BaseAddress";

    public static ServiceCollection ConfigureServices()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var services = new ServiceCollection();

        services.AddSingleton<IConfiguration>(configuration);

        services.AddHttpClient(ExternalUserHttpClient.ClientId, client =>
        {
            var baseAddress = configuration[BaseAddress];

            if (string.IsNullOrEmpty(baseAddress))
                throw new InvalidOperationException("BaseAddress not configured.");

            client.BaseAddress = new Uri(baseAddress);            
        })
            .AddPolicyHandler(GetRetryPolicy()); ;
        
        services.AddSingleton<UserService>();

        services.AddSingleton<IExternalUserClient, ExternalUserHttpClient>();

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
       .HandleTransientHttpError()
       .WaitAndRetryAsync(
           retryCount: 2,
           sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
       );
    }
}
