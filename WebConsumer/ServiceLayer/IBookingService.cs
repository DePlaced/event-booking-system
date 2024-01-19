using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;

namespace ServiceLayer
{
    public interface IBookingService
    {
        Task<Booking?> GetBooking(int id);

        Task<IEnumerable<Booking>> GetBookingsByUserId(int userId);

        Task<int> CreateBooking(Booking booking);

        Task<bool> UpdateBooking(int id, Booking booking);

        Task<bool> DeleteBooking(int id);
    }
}
