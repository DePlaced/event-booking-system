using System;
using Data.ModelLayer;
using RESTfulService.DTOs;

namespace RESTfulService.ModelConversion

{
	public static class BookingDTOConvert
	{
		public static BookingDTO ToBookingDTO(this Booking booking)
		{
			return new BookingDTO(
                booking.Id,
                booking.BookingDate,
                booking.BookingStatus,
                booking.TotalPrice,
                booking.SignRenterId,
                // Uses ToBookingLineDTO() static extension method from BookingLineDTOConvert.cs
                booking.Lines?.Select(l => l.ToBookingLineDTO()).ToList()
            );
        }

		public static Booking ToBooking(this BookingDTO bookingDTO)
		{
			return new Booking(
                bookingDTO.Id,
                bookingDTO.BookingDate,
                bookingDTO.BookingStatus,
                bookingDTO.TotalPrice,
                bookingDTO.SignRenterId,
                // Uses ToBookingLine() static extension method from BookingLineDTOConvert.cs
                bookingDTO.Lines?.Select(l => l.ToBookingLine()).ToList()
            );
        }
	}
}
