using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDSignClientDesktop.Model
{
    /// <summary>
    /// Represents an event with its properties such as name, date, description, price, availability status, and associated sign ID.
    /// </summary>
    public class Event
    {
        //Empty constructor
        public Event() { }

        //Constructor for showing and updating/deleting events
        public Event(int id, string name, DateTime eventDate, string description, decimal price, string availabilityStatus, int signId, byte[] rowId, Int64 rowIdBig)
        {
            Id = id;
            EventName = name;
            EventDate = eventDate;
            EventDescription = description;
            Price = price;
            AvailabilityStatus = availabilityStatus;
            SignId = signId;
			RowId = rowId;
			RowIdBig = rowIdBig;
		}

        //Constructor for saving events
        public Event(string name, DateTime eventDate, string description, decimal price, string availabilityStatus, int signId)
        {
            EventName = name;
            EventDate = eventDate;
            EventDescription = description;
            Price = price;
            AvailabilityStatus = availabilityStatus;
            SignId = signId;
        }

        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string EventDescription { get; set; }
        public decimal Price { get; set; }
        public string AvailabilityStatus { get; set; }
        public int SignId { get; set; }
        public byte[] RowId { get; set; }
        public Int64 RowIdBig { get; set; }

        public override string? ToString()
        {
            return $"Name: {EventName} - {AvailabilityStatus}, Date: {EventDate.ToString("dd/MM/yyyy")}";
        }
    }
}
