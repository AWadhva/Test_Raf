using ExternalUserService.Models;

public interface IExternalUserClient
{
    Task<User> GetUserByIdAsync(int userId);
    Task<List<User>> GetUsersByPageAsync(int page);
}
