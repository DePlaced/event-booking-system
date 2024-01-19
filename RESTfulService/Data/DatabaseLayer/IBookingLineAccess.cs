using Data.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
    public interface IBookingLineAccess
    {
        ///<summary>
        ///Gets a specific BookingLine from the database based on the id
        ///</summary>
        Task<BookingLine?> GetBookingLineById(int id);

        ///<summary>
        ///Gets all Bookings from the database
        ///</summary>
        Task<List<BookingLine>> GetBookingLinesByBookingId(int bookingId);

        ///<summary>
        ///Adds a new BookingLine row to the BookingLine table in the database
        ///</summary>
        Task<int> CreateBookingLine(BookingLine bookingLineToAdd);

        ///<summary>
        ///Updates BookingLine with a specific ID in the database
        ///</summary>
        Task<bool> UpdateBookingLine(BookingLine bookingLineToUpdate);

        ///<summary>
        ///Deletes BookingLine with given ID
        ///</summary>
        Task<bool> DeleteBookingLine(int id);
    }
}
