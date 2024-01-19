using BigDSignWebApp.ViewModels;
using ModelLayer;

namespace BigDSignWebApp.ModelConversion
{
    public static class BookingLineModelConvert
    {
        public static BookingLine ToBookingLine(this BookingLineModel bookingLineModel)
        {
            return new BookingLine(
                bookingLineModel.Id,
                bookingLineModel.SubPrice,
                bookingLineModel.BookingId,
                bookingLineModel.EventId,
                bookingLineModel.LineEvent.ToEvent()
            );
        }

        public static BookingLineModel ToBookingLineModel(this BookingLine bookingLine)
        {
            return new BookingLineModel(
                bookingLine.Id,
                bookingLine.SubPrice,
                bookingLine.BookingId,
                bookingLine.EventId,
                bookingLine.LineEvent.ToEventModel()
            );
        }
    }
}