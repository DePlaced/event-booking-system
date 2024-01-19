using ModelLayer;

namespace BusinessLogicLayer
{
    public interface IUserLogic
    {
        Task<User?> AuthenticateUser(string username, string password);
        Task<bool> RegisterUser(User user);
    }
}
