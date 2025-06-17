using ExternalUserService.Models;

namespace ExternalUserService;

public class UserService
{
    IGetUsers usersRepo;

    public UserService(IGetUsers usersRepo)
    {
        this.usersRepo = usersRepo;
    }

    public User GetUserById(int userId)
    {
        if (usersRepo.Users == null)
            return null;
        
        usersRepo.Users.TryGetValue(userId, out User user);
        return user;
    }

    public IEnumerable<User> GetAllUsers()
    {
        if (usersRepo.Users == null)
            return null;
        return usersRepo.Users.Values;
    }
}
