using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BigDSignWebApp.Cookies
{
    public class CookieUser
    {
        public CookieUser(int id, string username, string email, string fullName, string roles)
        {
            Id = id;
            Username = username;
            Email = email;
            FullName = fullName;
            Roles = roles;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Roles { get; set; }
    }
}
