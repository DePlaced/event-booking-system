using Dapper;
using Microsoft.Extensions.Configuration;
using Data.ModelLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
	public class BookingLineAccess : IBookingLineAccess
	{
		// The connection string used to connect to the database.
		private readonly string? _connectionString;

		// Initializes a new instance of the BookingLineAccess class.
		public BookingLineAccess(IConfiguration config)
		{
			// Retrieve the connection string from the configuration using the "SignRental" key.
			_connectionString = config.GetConnectionString("SignRental");
		}

		/// <summary>
		/// Gets a specific BookingLine from the database based on the id.
		/// </summary>
		public async Task<BookingLine?> GetBookingLineById(int id)
		{
			BookingLine bookingLine;
			// Defines query String.
			string queryString = "SELECT id, subPrice, bookingId, eventId FROM BookingLine WHERE id = @Id;";
			// Use a SqlConnection to connect to the database.
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				// Returns null if nothing found.
				IEnumerable<BookingLine> enumerable = await connection.QueryAsync<BookingLine>(queryString, new { Id = id });
				bookingLine = enumerable.First();
			}
			return bookingLine;
		}

		/// <summary>
		/// Gets all Bookings from the database.
		/// </summary>
		public async Task<List<BookingLine>> GetBookingLinesByBookingId(int bookingId)
		{
			List<BookingLine> bookingLines;
			// Defines query string.
			var queryString = "SELECT id, subPrice, bookingId, eventId FROM BookingLine Where bookingId = @BookingId;";
			// Use a SqlConnection to connect to the database.
			using (var connection = new SqlConnection(_connectionString))
			{
				// Query retrieves multiple rows from the database and map them to a list of BookingLine objects.
				// The result is then converted to a List and returned.
				IEnumerable<BookingLine> enumerable = await connection.QueryAsync<BookingLine>(queryString, new { BookingId = bookingId });
				bookingLines = enumerable.ToList();
			}
			return bookingLines;
		}

		/// <summary>
		/// Adds a new BookingLine row to the BookingLine table in the database.
		/// </summary>
		public async Task<int> CreateBookingLine(BookingLine bookingLineToAdd)
		{
			int insertedId;
			// Define query string.
			string queryString =
				"INSERT INTO BookingLine (subPrice, bookingId, eventId) " +
				"OUTPUT INSERTED.ID" +
				"VALUES(@SubPrice, @BookingId, @EventId);";
			// Use a SqlConnection to connect to the database.
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				// ExecuteScalar is used to retrieve a single value from the database.
				insertedId = await connection.ExecuteScalarAsync<int>(queryString, bookingLineToAdd);
			}
			// Return the ID of the newly created booking record.
			return insertedId;
		}

		/// <summary>
		/// Updates BookingLine with a specific ID in the database.
		/// </summary>
		public async Task<bool> UpdateBookingLine(BookingLine bookingLineToUpdate)
		{
			bool updated;
			// Define query string.
			string queryString = "UPDATE BookingLine SET subPrice = @SubPrice, bookingId = @BookingId, eventId = @EventId WHERE id = @Id;";
			// Use a SqlConnection to connect to the database.
			using (var connection = new SqlConnection(_connectionString))
			{
				// Returns the number of affected records, and the method returns true if at least one record is affected.
				updated = await connection.ExecuteAsync(queryString, bookingLineToUpdate) > 0;
			}
			return updated;
		}

		/// <summary>
		/// Deletes BookingLine with given ID.
		/// </summary>
		public async Task<bool> DeleteBookingLine(int id)
		{
			bool deleted;
			// Define query string.
			string queryString = "DELETE FROM Booking WHERE id = @Id;";
			// Use a SqlConnection to connect to the database.
			using (var connection = new SqlConnection(_connectionString))
			{
				// Returns the number of affected records, and the method returns true if at least one record is affected.
				deleted = await connection.ExecuteAsync(queryString, new { Id = id }) > 0;
			}
			return deleted;
		}
	}
}
