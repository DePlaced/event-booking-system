

using System.Text.Json.Serialization;

namespace BigDSignRestfulService.DTOs
{
    public class UserDTO
    {
        public UserDTO(int id, string firstName, string lastName, string email, string? username, string? password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Username = username;
            Password = password;
        }

        [JsonConstructor]
        public UserDTO(int id, UserRoleDTO? userRole, string firstName, string lastName, string email, string? username, string? password) : this(id, firstName, lastName, email, username, password)
        {
            UserRole = userRole;
        }

        public int Id { get; set; }
        public UserRoleDTO? UserRole { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
