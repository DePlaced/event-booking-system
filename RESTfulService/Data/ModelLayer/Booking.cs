using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelLayer
{
    public class Booking
    {

        public Booking(int id, DateTime bookingDate, string bookingStatus, decimal totalPrice, int signRenterId)
        {
            Id = id;
            BookingDate = bookingDate;
            BookingStatus = bookingStatus;
            TotalPrice = totalPrice;
            SignRenterId = signRenterId;
        }

        public Booking(int id, DateTime bookingDate, string bookingStatus, decimal totalPrice, int signRenterId, List<BookingLine>? lines) : this(id, bookingDate, bookingStatus, totalPrice, signRenterId)
        {
            Lines = lines;
        }

        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public string BookingStatus { get; set; }
        public decimal TotalPrice { get; set; }
        public int SignRenterId { get; set; }
        public List<BookingLine>? Lines { get; set; }
    }
}