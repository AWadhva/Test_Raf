using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace ExternalUserService.Tests2;

public class ExternalUserServiceTests_Basic
{
    ExternalUserService.IGetUsers usersRepo;
    public ExternalUserServiceTests_Basic()
    {
        var services = ExternalUserService.CompositionRoot.CompositionRoot.ConfigureServices();
        var provider = services.BuildServiceProvider();

        var temp = provider.GetRequiredService<UserFetcher>();
        usersRepo = provider.GetRequiredService<ExternalUserService.IGetUsers>();
        Thread.Sleep(3000); // TODO: clear this blot
    }

    [Fact]
    public void GetAllUserWorks()
    {
        usersRepo.Users.Count().ShouldBeGreaterThan(0);
    }
}
