using ExternalUserService.Models;

namespace ExternalUserService;

public interface ISetUsers
{
    void SetUsers(Dictionary<int, User> users);
}
