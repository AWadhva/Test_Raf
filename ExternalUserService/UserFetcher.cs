
using ExternalUserService.Models;

namespace ExternalUserService;

public class UserFetcher
{
    private readonly IExternalUserClient fetcher;
    private readonly ISetUsers users;

    public UserFetcher(IExternalUserClient fetcher, ISetUsers users)
    {
        this.fetcher = fetcher;
        this.users = users;

        Task.Run((Func<Task?>)(async () =>  {
            var users = await this.FetchUsers();
            this.users.SetUsers((Dictionary<int, User>)users);
            await Task.Delay(5 * 60 * 1000); // TODO: assuming data getting stale in 5 minutes
        }));
    }

    private async Task<Dictionary<int, User>> FetchUsers()
    {
        return await UserFetcherHelpers.FetchAllUsers(fetcher);
    }
}
