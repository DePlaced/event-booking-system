using ModelLayer;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BookingControl : IBookingControl{

        private readonly IBookingService _bookingService;

        public BookingControl(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<bool> CreateBooking(Booking booking)
        {
            return await _bookingService.CreateBooking(booking);
        }

        public async Task<bool> DeleteBooking(int id)
        {
           return await _bookingService.DeleteBooking(id);
        }

        public async Task<Booking?> GetBooking(int id)
        {
            return await _bookingService.GetBooking(id);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserId(int userId)
        {
           return await _bookingService.GetBookingsByUser(userId);
        }

        public async Task<bool> UpdateBooking(Booking booking, int id)
        {
            return await _bookingService.UpdateBooking(booking, id);
        }
    }
}
