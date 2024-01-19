using Newtonsoft.Json;

namespace ModelLayer
{
    public class UserRole
    {
        [JsonConstructor]
        public UserRole(int id, string role)
        {
            Id = id;
            Role = role;
        }
        public int Id { get; set; }
        public string Role { get; set; }
    }
}
