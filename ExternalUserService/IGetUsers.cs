using ExternalUserService.Models;

namespace ExternalUserService;

public interface IGetUsers
{    
    IReadOnlyDictionary<int, User> Users { get; }
}
