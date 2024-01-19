using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;

namespace BusinessLogicLayer
{
    public interface IBookingLineControl
    {
        Task<bool> CreateBookingLine(BookingLine bookingLine);
        Task<bool> DeleteBookingLine(int id);
        Task<BookingLine?> GetBookingLine(int id);
        Task<IEnumerable<BookingLine>> GetBookingLinesByBookingId(int bookingId);
        Task<bool> UpdateBookingLine(BookingLine bookingLine, int id);
    }
}
