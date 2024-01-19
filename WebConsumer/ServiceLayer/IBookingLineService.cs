using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;

namespace ServiceLayer
{
    public interface IBookingLineService
    {
        Task<BookingLine?> GetBookingLine(int id);

        Task<IEnumerable<BookingLine>> GetBookingLinesByBookingId(int bookingId);

        Task<int> CreateBookingLine(BookingLine bookingLine);

        Task<bool> UpdateBookingLine(int id, BookingLine bookingLine);

        Task<bool> DeleteBookingLine(int id);
    }
}
