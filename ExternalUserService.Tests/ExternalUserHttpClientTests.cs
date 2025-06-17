using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace ExternalUserService.Tests;

public class ExternalUserHttpClientTests
{
    ExternalUserHttpClient sut;
    public ExternalUserHttpClientTests()
    {
        var serviceCollection = new ServiceCollection();
        
        var provider = ExternalUserService.CompositionRoot.CompositionRoot.ConfigureServices();
        
        var _provider = serviceCollection.BuildServiceProvider();

        sut = _provider.GetRequiredService<ExternalUserHttpClient>();
        sut.ShouldNotBeNull();
    }

    [Fact]
    public void GetUserById_NominalCase()
    {        
    }

    [Fact]
    public void GetUsersByPage_NominalCase()
    { 
    }
}
