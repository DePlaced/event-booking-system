using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigDSignWebApp.ViewModels;
using ModelLayer;

namespace BusinessLogicLayer
{
    public interface IBookingLineLogic
    {
        Task<int> CreateBookingLine(BookingLineModel bookingLineModel);

        Task<bool> DeleteBookingLine(int id);

        Task<BookingLineModel?> GetBookingLine(int id);

        Task<IEnumerable<BookingLineModel>> GetBookingLinesByBookingId(int bookingId);

        Task<bool> UpdateBookingLine(int id, BookingLineModel bookingLineModel);
    }
}
