using ExternalUserService.Models;

namespace ExternalUserService;

public class UserService
{
    private readonly IExternalUserClient fetcher;

    public UserService(IExternalUserClient fetcher)
    {
        this.fetcher = fetcher;
    }

    public Task<Result<User>> GetUserById(int userId)
    {        
        return fetcher.GetUserByIdAsync(userId);        
    }

    public Task<IEnumerable<User>> GetAllUsers()
    {
        return UserFetcherHelpers.FetchAllUsersList(fetcher);
    }
}
