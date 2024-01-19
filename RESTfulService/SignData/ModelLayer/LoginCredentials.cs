using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignData.ModelLayer
{
    public class LoginCredentials
    {
        public LoginCredentials(int id, string username, string passwordHash, byte[] salt)
        {
            Id = id;
            Username = username;
            PasswordHash = passwordHash;
            Salt = salt;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public byte[] Salt { get; set; }
    }
}
