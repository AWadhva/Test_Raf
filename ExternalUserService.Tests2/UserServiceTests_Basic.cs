using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace ExternalUserService.Tests2;

public class UserServiceTests_Basic
{    
    UserService userService;
    public UserServiceTests_Basic()
    {
        var services = ExternalUserService.CompositionRoot.CompositionRoot.ConfigureServices();
        var provider = services.BuildServiceProvider();

        userService = provider.GetRequiredService<UserService>();        
    }

    [Fact]
    public async void GetAllUserAPIWorks()
    {
        (await userService.GetAllUsers()).ToList().Count().ShouldBeGreaterThan(0);
    }

    [Fact]
    public void GetUserByIdAPIWorks()
    {
        userService.GetUserById(userId: 1).ShouldNotBeNull();
    }
}
