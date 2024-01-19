using Data.ModelLayer;

namespace Data.DatabaseLayer
{
    public interface IBookingLineAccess
    {
        ///<summary>
        ///Find bookingLine on id
        ///</summary>
        Task<BookingLine?> GetBookingLineById(int id);

        ///<summary>
        ///Find bookingLines on bookingId
        ///</summary>
        Task<List<BookingLine>> GetBookingLinesByBookingId(int bookingId);

        ///<summary>
        ///Updates BookingLine with id
        ///</summary>
        Task<bool> UpdateBookingLine(BookingLine bookingLineToUpdate);

        ///<summary>
        ///Deletes BookingLine with id
        ///</summary>
        Task<bool> DeleteBookingLine(int id);
    }
}
