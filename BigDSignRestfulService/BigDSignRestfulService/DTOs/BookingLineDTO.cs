using System;
namespace BigDSignRestfulService.DTOs
{
    public class BookingLineDTO
    {
        public BookingLineDTO() { }

        public BookingLineDTO(int id, decimal subPrice, int bookingId, int eventId, EventDTO lineEvent)
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
        public EventDTO? LineEvent { get; set; }
    }
}

