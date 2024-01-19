namespace BigDSignRestfulService.DTOs
{ 
        public class SignDTO
        {
            
            public SignDTO(int id, string size, string resolution, string location, int stadiumId)
            {
                Id = id;
                Size = size;
                Resolution = resolution;
                Location = location;
                StadiumId = stadiumId;
            }

            public int Id { get; set; }
            public string Size { get; set; }
            public string Resolution { get; set; }
            public string Location { get; set; }
            public int StadiumId { get; set; }
        }
}
