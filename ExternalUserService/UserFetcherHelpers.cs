using ExternalUserService.Models;

namespace ExternalUserService
{
    internal static class UserFetcherHelpers
    {

        public static async Task<Dictionary<int, User>> FetchAllUsers(IExternalUserClient fetcher)
        {
            Dictionary<int, User> users = new();

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
}