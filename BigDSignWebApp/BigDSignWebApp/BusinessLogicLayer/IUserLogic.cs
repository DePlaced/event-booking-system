using ModelLayer;

namespace BusinessLogicLayer
{
    public interface IUserLogic
    {
        Task<User?> AuthenticateUser(string username, string password);
        Task RegisterUser(User user);
    }
}
