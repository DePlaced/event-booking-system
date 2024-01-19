using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using ModelLayer;

namespace BigDSignWebApp.Cookies
{
    public class CookieProvider : ICookieProvider
    {
        public HttpContext? HttpContext { get; set; }

        /// <summary>
        /// Create an authentication cookie, representing a login.
        /// </summary>
        /// <param name="user">The user to login.</param>
        /// <param name="rememberMe">Should the user be remebered across multiple sessions?</param>
        public async Task Login(User user, bool rememberMe)
        {
            if (HttpContext is null)
            {
                throw new InvalidOperationException($"Property this.{nameof(HttpContext)} cannot be null.");
            }
            if (user.UserRole is null)
            {
                throw new InvalidOperationException($"Property {nameof(user)}.{nameof(user.UserRole)} cannot be null.");
            }

            // List of claims (user values) of the user.
            IList<Claim> claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FullName", user.FullName),
                new Claim(ClaimTypes.Role, user.UserRole.Role),
            };

            // Cookie-based identity of the user.
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

            // Authentication cookie properties.
            AuthenticationProperties authProperties = new AuthenticationProperties()
            {
                // Allow refreshing of the authentication cookie.
                AllowRefresh = true,

                // The authentication cookie should persist across multiple sessions if the user checked the "Remember Me" checkbox.
                IsPersistent = rememberMe,

                // Time when the authentication cookie was issued.
                IssuedUtc = DateTimeOffset.UtcNow
            };

            // Create the authentication cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);
        }

        /// <summary>
        /// Clear an existing authentication cookie, resulting in a logout.
        /// </summary>
        public async Task Logout()
        {
            if (HttpContext is null)
            {
                throw new InvalidOperationException($"Property this.{nameof(HttpContext)} cannot be null.");
            }

            // Clear the authentication cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // Clear the current session so data that requires login is not available to a logged out user.
            HttpContext.Session.Clear();
        }

        /// <summary>
        /// Is the user logged in?
        /// </summary>
        /// <returns>True if logged in, false if not.</returns>
        public bool IsLoggedIn()
        {
            if (HttpContext is null)
            {
                throw new InvalidOperationException($"Property this.{nameof(HttpContext)} cannot be null.");
            }

            return HttpContext.User.Identity?.IsAuthenticated ?? false;
        }

        /// <summary>
        /// Get an user object containing relevant claims (user values), or null if logged out.
        /// </summary>
        /// <returns>The user object.</returns>
        public CookieUser? GetCookieUser()
        {
            if (HttpContext is null)
            {
                throw new InvalidOperationException($"Property this.{nameof(HttpContext)} cannot be null.");
            }

            CookieUser? cookieUser;
            if (IsLoggedIn())
            {
                // Create the cookie user object containing values retrieved from cookies.
                cookieUser = new CookieUser(
                    // ID.
                    int.Parse(HttpContext.User.FindFirstValue("Id") ?? ""),
                    // Name.
                    HttpContext.User.Identity?.Name ?? "",
                    // Email
                    HttpContext.User.FindFirstValue(ClaimTypes.Email) ?? "",
                    // Full name.
                    HttpContext.User.FindFirstValue("FullName") ?? "",
                    // Comma separated roles as a string.
                    string.Join(", ", HttpContext.User.FindAll(ClaimTypes.Role).Select(c => c.Value))
                );
            }
            else
            {
                cookieUser = null;
            }
            return cookieUser;
        }
    }
}
