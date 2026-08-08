using ComputerLaboratoryUsageMonitoringSystem.Models;

namespace ComputerLaboratoryUsageMonitoringSystem.Repositories;

public class UserRepository
{
    private static readonly List<User> Users = new();

    public void Add(User user)
    {
        user.Id = Users.Count + 1;
        Users.Add(user);
    }

    public User? Find(string username, string password)
    {
        return Users.FirstOrDefault(user =>
            user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
            user.Password == password);
    }

    public bool UsernameExists(string username)
    {
        return Users.Any(user =>
            user.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }
}
