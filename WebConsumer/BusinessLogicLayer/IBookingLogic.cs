using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;

namespace BusinessLogicLayer
{
    public interface IBookingControl
    {
        Task<bool> CreateBooking(Booking booking);
        Task<bool> DeleteBooking(int id);
        Task<Booking?> GetBooking(int id);
        Task<IEnumerable<Booking>> GetBookingsByUserId(int userId);
        Task<bool> UpdateBooking(Booking booking, int id);
    }
}
