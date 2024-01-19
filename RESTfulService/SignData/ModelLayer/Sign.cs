using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignData.ModelLayer
{
    public class Sign
    {

        public Sign() { }
        public Sign(string size, string resolution, string signLocation, int stadiumId)
        {
            Size = size;
            Resolution = resolution;
            SignLocation = signLocation;
            StadiumId = stadiumId;
        }

        public Sign(int id, string size, string resolution, string signLocation, int stadiumId)
        {
            Id = id;
            Size = size;
            Resolution = resolution;
            SignLocation = signLocation;
            StadiumId = stadiumId;
        }

        public List<Event>? Events { get; set; }
        public int Id { get; set; }
        public string Size { get; set; }
        public string Resolution { get; set; }
        public string SignLocation { get; set; }
        public int StadiumId { get; set; }
    }
}
