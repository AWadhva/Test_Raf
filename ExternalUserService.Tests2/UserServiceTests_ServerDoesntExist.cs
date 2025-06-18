using ExternalUserService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace ExternalUserService.Tests2;

public class UserServiceTests_ServerDoesntExist
{
    UserService userService;

    public UserServiceTests_ServerDoesntExist()
    {
        var services = ExternalUserService.CompositionRoot.CompositionRoot.ConfigureServices();
        services.AddHttpClient(ExternalUserHttpClient.ClientId, client =>
        {
            client.BaseAddress = new Uri("http://nonexistent.local");
        });

        var provider = services.BuildServiceProvider();

        userService = provider.GetRequiredService<UserService>();
    }
    
    //[Fact]
    //public async void GetAllUserAPI_YieldsExpectedResult()
    //{
    //    (await userService.GetAllUsers()).ToList().Count().ShouldBeGreaterThan(0);
    //}

    [Fact]
    public async void GetUserById_YieldsErrorResult()
    {
        (await userService.GetUserById(userId: 1)).IsSuccess.ShouldBeFalse();
    }

    [Fact]
    public async void GetAllUsers_YieldsErrorResult()
    {
        (await userService.GetAllUsers()).IsSuccess.ShouldBeFalse();
    }
}
