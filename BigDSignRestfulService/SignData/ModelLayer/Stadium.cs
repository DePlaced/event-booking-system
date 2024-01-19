using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignData.ModelLayer
{
    public class Stadium
    {
        public Stadium(int id, string stadiumName, string street, string city, int zipcode, int adminId)
        {
            Id = id;
            StadiumName = stadiumName;
            Street = street;
            City = city;
            Zipcode = zipcode;
            AdminId = adminId;
        }

        public List<Sign>? Signs { get; set; }
        public int Id { get; set; }
        public string StadiumName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int Zipcode { get; set; }
        public int AdminId { get; set; }

    }
}
