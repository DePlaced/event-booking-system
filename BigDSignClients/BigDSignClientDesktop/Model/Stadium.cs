using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDSignClientDesktop.Model
{
    public class Stadium
    {
        //Empty constructor
        public Stadium() { }

        //For saving a new Stadium
        public Stadium(string stadiumName, string street, string city, int zipcode, int adminId)
        {
            StadiumName = stadiumName;
            Street = street;
            City = city;
            Zipcode = zipcode;
            AdminId = adminId;
        }

        //For showing and updating the Stadiums
        public Stadium(int id, string stadiumName, string street, string city, int zipcode, int adminId)
        {
            Id = id;
            StadiumName = stadiumName;
            Street = street;
            City = city;
            Zipcode = zipcode;
            AdminId = adminId;
        }

        public int Id { get; set; }
        public string StadiumName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int Zipcode { get; set; }
        public int AdminId { get; set; }
    }
}
