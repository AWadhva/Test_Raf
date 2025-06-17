using ExternalUserService.Models;

namespace ExternalUserService;

public class UsersRepository : IGetUsers, ISetUsers
{
    public void SetUsers (Dictionary<int, User> users)
    { 
        {
            lock(lck)
            {
                this.users = users.AsReadOnly();
            }
        }            
    }

    IReadOnlyDictionary<int, User> IGetUsers.Users => users;

    IReadOnlyDictionary<int, User> users;
    object lck = new();
}
