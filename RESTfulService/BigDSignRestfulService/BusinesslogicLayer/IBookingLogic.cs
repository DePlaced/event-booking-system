using BigDSignRestfulService.DTOs;

namespace BigDSignRestfulService.BusinesslogicLayer
{
	public interface IBookingLogic
	{
		///<summary>
		///Get Booking by ID
		///</summary>
		Task<BookingDTO?> Get(int id);

		/// <summary>
		/// Get bookings by user ID
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		Task<List<BookingDTO>> GetAllByUserId(int userId);

		///<summary>
		///Create Booking
		///</summary>
		Task<int> Post(BookingDTO bookingToAdd);

		///<summary>
		///Update Booking by ID
		///</summary>
		Task<bool> Put(int id, BookingDTO bookingToUpdate);

		///<summary>
		///Delete Booking by ID
		///</summary>
		Task<bool> Delete(int id);
	}
}
