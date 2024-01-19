using Newtonsoft.Json;

namespace ModelLayer
{
    public class User
    {
        public User(int id, string firstName, string lastName, string email, string username, string? password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Username = username;
            Password = password;
        }

        [JsonConstructor]
        public User(int id, UserRole userRole, string firstName, string lastName, string email, string username, string? password) : this(id, firstName, lastName, email, username, password)
        {
            UserRole = userRole;
        }

        public int Id { get; set; }
        public UserRole? UserRole { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonIgnore]
        public string FullName => $"{FirstName} {LastName}".Trim();
        public string Email { get; set; }
        public string Username { get; set; }
        public string? Password { get; set; }
    }
}
