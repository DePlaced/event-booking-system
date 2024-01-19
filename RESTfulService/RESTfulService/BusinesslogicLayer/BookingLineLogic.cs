using RESTfulService.DTOs;
using Data.DatabaseLayer;
using Data.ModelLayer;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using RESTfulService.ModelConversion;

namespace RESTfulService.BusinesslogicLayer
{
	/// <summary>
	/// Service class for handling operations related to booking line entities.
	/// </summary>
	public class BookingLineLogic : IBookingLineLogic
	{
		/// <summary>
		/// The data access layer for booking line entities.
		/// </summary>
		private readonly IBookingLineAccess _bookingLineAccess;
		private readonly IEventAccess _eventAccess;

		/// <summary>
		/// Initializes a new instance of the BookingLineService class.
		/// </summary>
		/// <param name="bookingLineAccess">The booking line access object used for database operations.</param>
		public BookingLineLogic(IBookingLineAccess bookingLineAccess, IEventAccess eventAccess)
		{
			_bookingLineAccess = bookingLineAccess;
			_eventAccess = eventAccess;
		}

		///<summary>
		///Deletes a BookingLine with given ID. 
		///</summary>
		public async Task<bool> Delete(int id)
		{
			// Delete the booking line using the data access layer.
			return await _bookingLineAccess.DeleteBookingLine(id);
		}

		///<summary>
		///Gets a BookingLine with the given ID.
		///</summary>
		public async Task<BookingLineDTO?> Get(int id)
		{
			// Retrieve the booking line using the data access layer.
			BookingLine? foundBookingLine = await _bookingLineAccess.GetBookingLineById(id);

			// If the booking line is found, convert it to a DTO and return.
			if (foundBookingLine != null)
			{
				return foundBookingLine.ToBookingLineDTO();
			}
			return null;
		}

		///<summary>
		///Gets all BookingLines.
		///</summary>
		public async Task<List<BookingLineDTO>> GetAll(int bookingId)
		{
			// Retrieve all booking lines using the data access layer.
			List<BookingLine>? foundBookingLines = await _bookingLineAccess.GetBookingLinesByBookingId(bookingId);

			// Convert to DTO list
			var foundBookingLinesDTO = foundBookingLines.Select(line => line.ToBookingLineDTO()).ToList();

			// Create and start tasks for each lineDTO in foundBookingLinesDTO to assign an eventDTO
			var tasks = foundBookingLinesDTO.Select(async lineDTO =>
			{
				var eventDTO = await _eventAccess.GetEventById(lineDTO.EventId);
				lineDTO.LineEvent = eventDTO.ToEventDTO();
			}).ToArray(); // ToArray is used to execute the Select statement and start the tasks

			// Wait for all tasks to complete
			await Task.WhenAll(tasks);

			return foundBookingLinesDTO;
		}

		///<summary>
		///Updates a BookingLine with the given ID.
		///</summary>
		public async Task<bool> Put(int id, BookingLineDTO bookingLineToUpdate)
		{
			BookingLine bookingLine = bookingLineToUpdate.ToBookingLine();
			return await _bookingLineAccess.UpdateBookingLine(bookingLine);
		}
	}
}
