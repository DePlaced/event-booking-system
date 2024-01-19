using ModelLayer;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BookingLineControl : IBookingLineControl
    {

        private readonly IBookingLineService _bookingLineService;

        public BookingLineControl(IBookingLineService bookingLineService)
        {
            _bookingLineService = bookingLineService;
        }

        public async Task<bool> CreateBookingLine(BookingLine bookingLine)
        {
            return await _bookingLineService.CreateBookingLine(bookingLine);
        }

        public async Task<bool> DeleteBookingLine(int id)
        {
           return await _bookingLineService.DeleteBookingLine(id);
        }

        public async Task<BookingLine?> GetBookingLine(int id)
        {
            return await _bookingLineService.GetBookingLine(id);
        }

        public async Task<IEnumerable<BookingLine>> GetBookingLinesByBookingId(int bookingId)
        {
           return await _bookingLineService.GetBookingLinesByBookingId(bookingId);
        }

        public async Task<bool> UpdateBookingLine(BookingLine bookingLine, int id)
        {
            return await _bookingLineService.UpdateBookingLine(bookingLine, id);
        }
    }
}
