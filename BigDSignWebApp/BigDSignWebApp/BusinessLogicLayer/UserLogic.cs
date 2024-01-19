using ModelLayer;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserService _userService;

        public UserLogic(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<User?> AuthenticateUser(string username, string password)
        {
            return await _userService.AuthenticateUser(username, password);
        }

        public async Task RegisterUser(User user)
        {
            await _userService.RegisterUser(user);
        }
    }
}
