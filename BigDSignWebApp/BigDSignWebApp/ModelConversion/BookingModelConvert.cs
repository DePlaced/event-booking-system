using BigDSignWebApp.ViewModels;
using ModelLayer;

namespace BigDSignWebApp.ModelConversion
{
    public static class BookingModelConvert
    {
        public static Booking ToBooking(this BookingModel bookingModel)
        {
            return new Booking(
                bookingModel.Id,
                bookingModel.BookingDate,
                bookingModel.BookingStatus,
                bookingModel.TotalPrice,
                bookingModel.SignRenterId
            );
        }

        public static BookingModel ToBookingModel(this Booking booking)
        {
            return new BookingModel(
                booking.Id,
                booking.BookingDate,
                booking.BookingStatus,
                booking.TotalPrice,
                booking.SignRenterId
            );
        }
    }
}