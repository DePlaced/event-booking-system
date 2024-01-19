using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignData.ModelLayer
{
    public class BookingLine
    {
        public BookingLine() { }

        public BookingLine(decimal subPrice, int bookingId, int eventId, Event? lineEvent )
        {
            SubPrice = subPrice;
            BookingId = bookingId;
            EventId = eventId;
            LineEvent = lineEvent;
        }

        public BookingLine(int id, decimal subPrice, int bookingId, int eventId, Event? lineEvent) 
        {
            Id = id;
            SubPrice = subPrice;
            BookingId = bookingId;
            EventId = eventId;
			LineEvent = lineEvent;
		}

        public int Id { get; set; }
        public decimal SubPrice { get; set; }
        public int BookingId { get; set; }
        public int EventId { get; set; }
        public Event? LineEvent { get; set; }

    }
}
