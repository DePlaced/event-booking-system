using ModelLayer;

namespace ServiceLayer
{
    public interface IUserService
    {
        Task<User?> AuthenticateUser(string username, string password);
        Task RegisterUser(User user);
    }
}
