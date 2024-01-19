using SignData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignData.DatabaseLayer
{
    public interface IBookingAccess
    {
        ///<summary>
        ///Gets a specific booking from the database based on the id
        ///</summary>
        Task<Booking?> GetBookingById(int id);

        ///<summary>
        ///Gets all Bookings by user ID from the database
        ///</summary>
        Task<List<Booking>> GetAllBookingsByUserId(int userId);

        ///<summary>
        ///Adds a new Booking row to the Booking table in the database
        ///</summary>
        Task<int> CreateBooking(Booking bookingToAdd, int time = 0);

        ///<summary>
        ///Updates Booking with given ID
        ///</summary>
        Task<bool> UpdateBooking(int id, Booking bookingToUpdate);

        ///<summary>
        ///Deletes booking with given input ID
        ///</summary>
        Task<bool> DeleteBookingById(int id);
    }
}
