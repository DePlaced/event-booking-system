using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SignData.ModelLayer
{
    public class UserRole
    {
        public UserRole(int id, string role)
        {
            Id = id;
            Role = role;
        }

        public int Id { get; set; }
        public string Role { get; set; }
    }
}
