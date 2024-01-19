using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDSignClientDesktop.Model
{
    public class Sign
    {

        //empty contructor
        public Sign() { }

        //for saving new sign
        public Sign(string size, string resolution, string location, int stadiumId)
        {
            Size = size;
            Resolution = resolution;
            Location = location;
            StadiumId = stadiumId;
        }

        //for showing and updating signs
        public Sign(int id, string size, string resolution, string location, int stadiumId)
        {
            Id = id;
            Size = size;
            Resolution = resolution;
            Location = location;
            StadiumId = stadiumId;
        }

        public int Id { get; set; }
        public string? Size { get; set; }
        public string? Resolution { get; set; }
        public string? Location { get; set; }
        public int StadiumId { get; set; }

        public override string? ToString()
        {
            return $"Location: {Location}, Resolution: {Resolution}, Size: {Size}";
        }

    }

}
