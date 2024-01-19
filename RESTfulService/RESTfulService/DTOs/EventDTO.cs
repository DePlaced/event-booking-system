using System.Text.Json.Serialization;

namespace RESTfulService.DTOs
{
    public class EventDTO
    {
		public EventDTO() { }

		[JsonConstructor]
		public EventDTO(string? eventName, DateTime eventDate, string? eventDescription, decimal price, string? availabilityStatus, int signId)
		{
			EventName = eventName;
			EventDate = eventDate;
			EventDescription = eventDescription;
			Price = price;
			AvailabilityStatus = availabilityStatus;
			SignId = signId;
		}

		public EventDTO(int id, string eventName, DateTime eventDate, string eventDescription, decimal price, string availabilityStatus, int signId, byte[] rowId, Int64 rowIdBig)
		{
			Id = id;
			EventName = eventName;
			EventDate = eventDate;
			EventDescription = eventDescription;
			Price = price;
			AvailabilityStatus = availabilityStatus;
			SignId = signId;
			RowId = rowId;
			RowIdBig = rowIdBig;
		}
		public int Id { get; set; }
        public string? EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string? EventDescription { get; set; }
        public decimal Price { get; set; }
        public string? AvailabilityStatus { get; set; }
        public int SignId { get; set; }
        public byte[]? RowId { get; set; }
        public Int64 RowIdBig { get; set; }
    }

}
