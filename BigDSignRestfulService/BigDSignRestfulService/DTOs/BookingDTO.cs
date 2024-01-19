using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BigDSignRestfulService.DTOs
{
	public class BookingDTO
	{
        public BookingDTO() { }

		public BookingDTO(int id, DateTime bookingDate, string bookingStatus, decimal totalPrice, int signRenterId)
		{
			Id = id;
			BookingDate = bookingDate;
			BookingStatus = bookingStatus;
			TotalPrice = totalPrice;
			SignRenterId = signRenterId;
		}

        public BookingDTO(int id, DateTime bookingDate, string bookingStatus, decimal totalPrice, int signRenterId, List<BookingLineDTO>? lines)
        {
            Id = id;
            BookingDate = bookingDate;
            BookingStatus = bookingStatus;
            TotalPrice = totalPrice;
            SignRenterId = signRenterId;
            Lines = lines;
        }

        public int Id { get; set; }
		public DateTime BookingDate { get; set; }
		public string BookingStatus { get; set; }
		public decimal TotalPrice { get; set; }
		public int SignRenterId { get; set; }
		public List<BookingLineDTO>? Lines { get; set; }
    }
}

