using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDSignWebApp.ViewModels
{
    public class BookingLineModel
    {
        public int Id { get; set; }
        public decimal SubPrice { get; set; }
        public int BookingId { get; set; }
        public int EventId { get; set; }

        public EventModel? LineEvent { get; set; }

        public BookingLineModel() { }

        //show, update
        public BookingLineModel(int id, decimal subPrice, int bookingId, int eventId, EventModel lineEvent)
        {
            Id = id;
            SubPrice = subPrice;
            BookingId = bookingId;
            EventId = eventId;
            LineEvent = lineEvent;
        }

        //create
        public BookingLineModel(decimal subPrice, int eventId)
        {
            SubPrice = subPrice;
            EventId = eventId;
        }
    }
}
