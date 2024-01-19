using BigDSignRestfulService.DTOs;
using SignData.DatabaseLayer;
using SignData.ModelLayer;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using BigDSignRestfulService.ModelConversion;
using SignData.Exceptions;

namespace BigDSignRestfulService.BusinesslogicLayer
{
    /// <summary>
    /// Logic class for handling operations related to booking entities.
    /// </summary>
    public class BookingLogic : IBookingLogic
    {
        /// <summary>
        /// The data access layer for booking entities.
        /// </summary>
        private readonly IBookingAccess _bookingAccess;

        /// <summary>
        /// Initializes a new instance of the BookingLogic class.
        /// </summary>
        /// <param name="bookingAccess">The booking access object used for database operations.</param>
        public BookingLogic(IBookingAccess bookingAccess)
        {
            _bookingAccess = bookingAccess;
        }

        /// <summary>
        /// Create Booking
        /// </summary>
        public async Task<int> Post(BookingDTO bookingToAdd)
        {
            if (bookingToAdd.Lines is null || bookingToAdd.Lines.Count == 0)
            {
                throw new BadRequestException("Booking must have one or more booking lines.");
            }

            return await _bookingAccess.CreateBooking(bookingToAdd.ToBooking());
        }

        /// <summary>
        /// Delete Booking by ID
        /// </summary>
        public async Task<bool> Delete(int id)
        {
            // Delete the booking using the data access layer.
            return await _bookingAccess.DeleteBookingById(id);
        }

        /// <summary>
        /// Get Booking by ID
        /// </summary>
        public async Task<BookingDTO?> Get(int id)
        {
            // Retrieve the booking using the data access layer.
            Booking? foundBooking = await _bookingAccess.GetBookingById(id);

            // If the booking is found, convert it to a DTO and return.
            if (foundBooking != null)
            {
                return foundBooking.ToBookingDTO();
            }

            return null;
        }

        /// <summary>
        /// Get all Bookings by user ID
        /// </summary>
        public async Task<List<BookingDTO>?> GetAllByUserId(int userId)
        {
            // Retrieve all bookings using the data access layer.
            List<Booking>? foundBookings = await _bookingAccess.GetAllBookingsByUserId(userId);

            // If bookings are found, convert the collection to DTOs and return.
            if (foundBookings != null)
            {
                // Conver to DTO list
                List<BookingDTO> foundBookingDTOs = foundBookings.Select( b => b.ToBookingDTO()).ToList();
                return foundBookingDTOs;
            }

            return null;
        }

        /// <summary>
        /// Update Booking by ID
        /// </summary>
        public async Task<bool> Put(int id, BookingDTO bookingToUpdate)
        {
            return await _bookingAccess.UpdateBooking(id, bookingToUpdate.ToBooking());
        }
    }
}
