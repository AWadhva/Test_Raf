
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
        
        Task.Run(async () =>  {
            var users = await FetchUsers();            
            this.users.SetUsers(users);
            await Task.Delay(5 * 60 * 1000); // TODO: assuming data getting stale in 5 minutes
        });
    }

    private async Task<Dictionary<int, User>> FetchUsers()
    {
        var allUsers = new Dictionary<int, User>();

        int page = 1;
        Dictionary<int , User> users = new();

        do
        {
            var userBatch = await fetcher.GetUsersByPageAsync(page);
            if (userBatch == null)
                return allUsers;

            userBatch.ForEach(x =>
            {
                if (users.TryGetValue(x.Id, out _))
                {
                    // TODO: log message                    
                }
                else
                    users[x.Id] = x;
            }
            );
            
            page++;
        }
        while (users.Count > 0);

        return allUsers;
    }
}
