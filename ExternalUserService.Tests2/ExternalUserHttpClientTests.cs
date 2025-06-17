using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace ExternalUserService.Tests;

public class ExternalUserHttpClientTests
{
    ExternalUserHttpClient sut;
    public ExternalUserHttpClientTests()
    {        
        var services = ExternalUserService.CompositionRoot.CompositionRoot.ConfigureServices();
        services.AddTransient<ExternalUserHttpClient>();

        var provider = services.BuildServiceProvider();

        sut = provider.GetRequiredService<ExternalUserHttpClient>();
        sut.ShouldNotBeNull();        
    }

    [Fact]
    public async void GetUserById_NominalCase()
    {
        var lst = await sut.GetUsersByPageAsync(page: 1);
        lst.ShouldNotBeNull();
        lst.ShouldNotBeEmpty();
    }

    [Fact]
    public async void GetUsersByPage_NominalCase()
    {
        var user = await sut.GetUserByIdAsync(userId:1);
        user.ShouldNotBeNull();
    }
}
