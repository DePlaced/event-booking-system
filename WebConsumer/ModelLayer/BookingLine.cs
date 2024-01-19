using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class BookingLine
    {
        public int Id { get; set; }
        public decimal SubPrice { get; set; }
        public int BookingId { get; set; }
        public int EventId { get; set; }
        public Event? LineEvent { get; set; }

        public BookingLine() { }

        //to show and update
        public BookingLine(int id, decimal subPrice, int bookingId, int eventId, Event lineEvent)
        {
            Id = id;
            SubPrice = subPrice;
            BookingId = bookingId;
            EventId = eventId;
            LineEvent = lineEvent;
        }

        //to save
        public BookingLine(decimal subPrice, int eventId, Event lineEvent)
        {
            SubPrice = subPrice;
            EventId = eventId;
            LineEvent = lineEvent;
        }
    }
}
