using ExternalUserService.Models;

namespace ExternalUserService
{
    internal static class UserFetcherHelpers
    {
        public static async Task<Result<IEnumerable<User>>> FetchAllUsersList(IExternalUserClient fetcher)
        {
            var result = await FetchAllUsers(fetcher);
            if (result.IsSuccess)
            {
                return Result<IEnumerable<User>>.Success(result.Value.Values);
            }
            else
            {
                return Result<IEnumerable<User>>.Failure(result.Error);
            }
        }

        public static async Task<Result<Dictionary<int, User>>> FetchAllUsers(IExternalUserClient fetcher)
        {
            Dictionary<int, User> users = new();

            int page = 0;

            var userBatch = await fetcher.GetUsersByPageAsync(page);
            if (!userBatch.IsSuccess)
                return Result<Dictionary<int, User>>.Failure(userBatch.Error);

            int totalPages = (int)userBatch.Value.TotalPages;

            while (true)
            {
                userBatch.Value.Users.ForEach(x =>
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
                    if (!userBatch.IsSuccess)
                        return Result<Dictionary<int, User>>.Failure(userBatch.Error);
                }
            }

            return Result < Dictionary<int, User>>.Success(users);
        }
    }
}