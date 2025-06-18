
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
        Dictionary<int , User> users = new();

        int page = 0;

        var userBatch = await fetcher.GetUsersByPageAsync(page);
        if (userBatch.Users == null || userBatch.TotalPages == null)
            return users;

        int totalPages = (int)userBatch.TotalPages;

        while (true)
        {
            userBatch.Users.ForEach(x =>
            {
                if (users.TryGetValue(x.Id, out _))
                {
                    // TODO: Duplicate. log message
                }
                else
                    users.Add(x.Id, x);
            }
            );
            
            page++;
            if (page == totalPages)
                break;
            else
            {
                userBatch = await fetcher.GetUsersByPageAsync(page);
                if (userBatch.Users == null)
                    break;
            }
        }

        return users;
    }
}
