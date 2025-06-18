using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace ExternalUserService.Tests2;

public class UserServiceTests_Basic
{
    ExternalUserService.IGetUsers usersRepo;
    UserService userService;
    public UserServiceTests_Basic()
    {
        var services = ExternalUserService.CompositionRoot.CompositionRoot.ConfigureServices();
        var provider = services.BuildServiceProvider();

        var temp = provider.GetRequiredService<UserFetcher>();
        usersRepo = provider.GetRequiredService<ExternalUserService.IGetUsers>();
        userService = provider.GetRequiredService<UserService>();
        Thread.Sleep(3000); // TODO: clear this blot
    }

    [Fact]
    public void UserFetcherWorks()
    {
        usersRepo.Users.Count().ShouldBeGreaterThan(0);
    }

    [Fact]
    public void GetAllUserAPIWorks()
    {
        userService.GetAllUsers().Count().ShouldBeGreaterThan(0);
    }

    [Fact]
    public void GetUserByIdAPIWorks()
    {
        userService.GetUserById(userId: 1).ShouldNotBeNull();
    }
}
