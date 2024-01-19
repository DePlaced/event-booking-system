using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using ModelLayer;
using System.Security.Claims;

namespace BigDSignWebApp.Cookies
{
    public interface ICookieProvider
    {
        HttpContext? HttpContext { get; set; }

        /// <summary>
        /// Create an authentication cookie, representing a login.
        /// </summary>
        /// <param name="user">The user to login.</param>
        /// <param name="rememberMe">Should the user be remebered across multiple sessions?</param>
        Task Login(User user, bool rememberMe);

        /// <summary>
        /// Clear an existing authentication cookie, resulting in a logout.
        /// </summary>
        Task Logout();

        /// <summary>
        /// Is the user logged in?
        /// </summary>
        /// <returns>True if logged in, false if not.</returns>
        bool IsLoggedIn();

        /// <summary>
        /// Get an user object containing relevant claims (user values), or null if logged out.
        /// </summary>
        /// <returns>The user object.</returns>
        CookieUser? GetCookieUser();
    }
}
