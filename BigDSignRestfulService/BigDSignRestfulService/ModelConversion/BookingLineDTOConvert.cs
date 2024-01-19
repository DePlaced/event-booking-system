using System;
using SignData.ModelLayer;
using BigDSignRestfulService.DTOs;

namespace BigDSignRestfulService.ModelConversion
{
	public static class BookingLineDTOConvert
	{
		public static BookingLineDTO ToBookingLineDTO(this BookingLine bookingLine)
		{
			return new BookingLineDTO(
                bookingLine.Id,
                bookingLine.SubPrice,
                bookingLine.BookingId,
                bookingLine.EventId,
                bookingLine.LineEvent?.ToEventDTO()
            );
        }

		public static BookingLine ToBookingLine(this BookingLineDTO bookingLineDTO)
		{
            return new BookingLine(
                bookingLineDTO.Id,
                bookingLineDTO.SubPrice,
                bookingLineDTO.BookingId,
                bookingLineDTO.EventId,
				bookingLineDTO.LineEvent?.ToEvent()
			);
        }
	}
}
