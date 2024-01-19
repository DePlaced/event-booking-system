using System.Text.Json.Serialization;

namespace BigDSignRestfulService.DTOs
{
    public class UserRoleDTO
    {
        [JsonConstructor]
        public UserRoleDTO(int id, string role)
        {
            Id = id;
            Role = role;
        }

        public int Id { get; set; }
        public string Role { get; set; }
    }
}
