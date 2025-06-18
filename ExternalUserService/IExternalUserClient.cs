using ExternalUserService.Models;

public interface IExternalUserClient
{
    Task<Result<User>> GetUserByIdAsync(int userId);
    Task<Result<(int TotalPages, List<User> Users)>> GetUsersByPageAsync(int page);
}
