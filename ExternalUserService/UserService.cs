using ExternalUserService.Models;
using Utils;

namespace ExternalUserService;

public class UserService
{
    private readonly IExternalUserClient fetcher;
    private readonly ICacheProvider<int, User> cache;

    TimeSpan ts = TimeSpan.FromMinutes(5);

    public UserService(IExternalUserClient fetcher, ICacheProvider<int, User> cache)
    {
        this.fetcher = fetcher;
        this.cache = cache;
    }

    public Task<Result<User>> GetUserById(int userId)
    {
        if (cache.TryGetValue(userId, out var cachedUser))
        {
            return Task.FromResult(Result<User>.Success(cachedUser));
        }

        return fetcher.GetUserByIdAsync(userId);        
    }

    public async Task<Result<IEnumerable<User>>> GetAllUsers()
    {        
        var result = await UserFetcherHelpers.FetchAllUsersList(fetcher);
        if (result.IsSuccess)
        {
            cache.Set(result.Value, ts);
            return Result<IEnumerable<User>>.Success(result.Value.Values);
        }
        else            
            return Result<IEnumerable<User>>.Failure(result.Error);        
    }
}
