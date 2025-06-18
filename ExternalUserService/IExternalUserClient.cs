using ExternalUserService.Models;

public interface IExternalUserClient
{
    Task<User> GetUserByIdAsync(int userId);
    Task<(int? TotalPages, List<User> Users)> GetUsersByPageAsync(int page);
}
