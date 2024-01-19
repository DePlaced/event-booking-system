using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignData.ModelLayer
{
    public class User
    {
        public User(int id, string firstName, string lastName, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public User(int id, string firstName, string lastName, string email, LoginCredentials? loginCredentials) : this(id, firstName, lastName, email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            LoginCredentials = loginCredentials;
        }

        public User(int id, UserRole? userRole, string firstName, string lastName, string email, LoginCredentials? loginCredentials) : this(id, firstName, lastName, email, loginCredentials)
        {
            UserRole = userRole;
        }

        public int Id { get; set; }
        public UserRole? UserRole { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public LoginCredentials? LoginCredentials { get; set; }
    }
}
