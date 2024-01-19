using RESTfulService.DTOs;

namespace RESTfulService.BusinesslogicLayer
{
	public interface IBookingLineLogic
	{
		///<summary>
		///Gets a BookingLine with the given ID.
		///</summary>
		Task<BookingLineDTO?> Get(int id);

		///<summary>
		///Gets all BookingLines.
		///</summary>
		Task<List<BookingLineDTO>> GetAll(int bookingId);

		///<summary>
		///Updates a BookingLine with the given ID.
		///</summary>
		Task<bool> Put(int id, BookingLineDTO bookingLineToUpdate);

		///<summary>
		///Deletes a BookingLine with given ID. 
		///</summary>
		Task<bool> Delete(int id);
	}
}
