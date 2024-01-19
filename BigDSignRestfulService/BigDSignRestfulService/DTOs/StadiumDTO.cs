using SignData.ModelLayer;

namespace BigDSignRestfulService.DTOs
{
    public class StadiumDTO
    {
        public StadiumDTO(int id, string stadiumName, string street, string city, int zipcode, int adminId)
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
