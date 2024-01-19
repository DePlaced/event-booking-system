using Dapper;
using Microsoft.Extensions.Configuration;
using Data.Exceptions;
using Data.ModelLayer;
using System.Data;
using System.Data.SqlClient;

namespace Data.DatabaseLayer
{
	public class BookingAccess : IBookingAccess
	{
		private readonly string? _connectionString;

		public BookingAccess(IConfiguration config)
		{
			_connectionString = config.GetConnectionString("SignRental");
		}

		/// <summary>
		/// Gets a specific booking from the database based on the id.
		/// </summary>
		public async Task<Booking?> GetBookingById(int id)
		{
			Booking? booking;
			// Defines query string.
			string queryString = "SELECT id, bookingDate, bookingStatus, totalPrice, signRenterId FROM Booking where id = @Id;";
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				// Returns null if nothing found.
				IEnumerable<Booking> enumerable = await connection.QueryAsync<Booking>(queryString, new { Id = id });
				booking = enumerable.FirstOrDefault();

			}
			return booking;
		}

		/// <summary>
		/// Gets all Bookings from the database.
		/// </summary>
		public async Task<List<Booking>> GetAllBookingsByUserId(int userId)
		{
			List<Booking> bookings;
			// Defines query string.
			var queryString = "SELECT id, bookingDate, bookingStatus, totalPrice, signRenterId FROM Booking WHERE signRenterId = @UserId;";
			using (var connection = new SqlConnection(_connectionString))
			{
				IEnumerable<Booking> enumerable = await connection.QueryAsync<Booking>(queryString, new { UserId = userId });
				bookings = enumerable.ToList();
			}
			return bookings;
		}

		/// <summary>
		/// Adds new booking as well as related bookingLines to the database and updates related events to 'Sold out' all in  one transaction
		/// </summary>
		/// <param name="bookingToAdd">The booking to be added</param>
		/// <param name="time">Parameter only for test purpose</param>
		/// <returns>Returns the id of the created booking</returns>
		public async Task<int> CreateBooking(Booking bookingToAdd, int time = 0)
		{
			int insertedId = -1;
			int numberOfRowsUpdated = 0;
			string queryString =
				"INSERT INTO Booking (bookingDate, bookingStatus, totalPrice, signRenterId) " +
				"OUTPUT INSERTED.ID " +
				"VALUES(@BookingDate, @BookingStatus, @TotalPrice, @SignRenterId)";

			string queryStringList = "INSERT INTO BookingLine (subPrice, bookingId, eventId) " +
				"OUTPUT INSERTED.ID " +
				"VALUES(@SubPrice, @BookingId, @EventId);";
			
			string queryStringUpdateEvent = "UPDATE event SET availabilityStatus = 'Sold out' " +
				"WHERE id = @EventId AND (cast(@RowIdBig as binary(8)) = rowId);";
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				await con.OpenAsync();
				// Begin a transaction.
				using (SqlTransaction transaction = con.BeginTransaction(IsolationLevel.ReadUncommitted))
				{
					try
					{
						// Insert booking and returns bookingId.
						insertedId = await con.ExecuteScalarAsync<int>(queryString, bookingToAdd, transaction);

						// If the booking object contains a list, it will input the list in the database.
						if (bookingToAdd.Lines != null)
						{
							// Inputs a list of bookinglines in the database.
							await con.ExecuteAsync(queryStringList, bookingToAdd.Lines.Select(l => new BookingLine(l.SubPrice, insertedId, l.EventId, l.LineEvent)), transaction);

							//For test purpose
							if (time > 0) {
								await Task.Delay(time);
							}

							//updates each event in database to Sold out where EventId and RowId matches witht the events attached to the booking lines
							foreach (var bookingLine in bookingToAdd.Lines)
							{
								numberOfRowsUpdated += await con.ExecuteAsync(queryStringUpdateEvent, new { EventId = bookingLine.EventId, RowIdBig = bookingLine.LineEvent.RowIdBig }, transaction);
							}
							//only commits if number of updated events matches number of booking lines in the booking
							if (numberOfRowsUpdated == bookingToAdd.Lines.Count)
							{
								transaction.Commit();
							}
							else
							{
								throw new BadRequestException("Event data is outdated or aldready booked. Refresh and try again.");
							}
						}
						else
						{
							throw new BadRequestException("Remember to add events before booking.");
						}
					}
					catch (Exception)
					{
						transaction.Rollback();
						throw;
					}
				}
			}
			return insertedId;
		}

		/// <summary>
		/// Updates Booking with given ID.
		/// </summary>
		public async Task<bool> UpdateBooking(int id, Booking bookingToUpdate)
		{
			bool updated;
			string queryString = "UPDATE Booking SET bookingDate = @BookingDate, bookingStatus = @BookingStatus, totalPrice = @TotalPrice, signrenterId = @SignRenterId WHERE id = @Id;";
			// Creates new object with given ID.
			var bookingWithId = new Booking(id, bookingToUpdate.BookingDate, bookingToUpdate.BookingStatus, bookingToUpdate.TotalPrice, bookingToUpdate.SignRenterId);
			using (var connection = new SqlConnection(_connectionString))
			{
				updated = await connection.ExecuteAsync(queryString, bookingWithId) > 0;
			}
			return updated;
		}

		/// <summary>
		/// Deletes booking with given input ID.
		/// </summary>
		public async Task<bool> DeleteBookingById(int id)
		{
			bool deleted;
			string queryStringGetEventId = "SELECT eventId FROM BookingLine WHERE bookingId = @Id";
			string queryStringUpdateEvent = "UPDATE Event SET availabilityStatus = 'Available' WHERE id IN @EventIds";
			string queryStringDeleteBooking = "DELETE FROM Booking WHERE id = @Id;";
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				using (var transaction = connection.BeginTransaction())
				{
					try
					{
						// Fetch event IDs associated with the booking.
						IEnumerable<int> eventIds = connection.Query<int>(queryStringGetEventId, new { Id = id }, transaction);
						if (eventIds != null && eventIds.Any())
						{
							// Update availability status for events associated with the deleted booking.
							await connection.ExecuteAsync(queryStringUpdateEvent, new { EventIds = eventIds }, transaction);
						}
						// Delete the booking.
						deleted = await connection.ExecuteAsync(queryStringDeleteBooking, new { Id = id }, transaction) > 0;
						transaction.Commit();
					}
					catch (Exception)
					{
						transaction.Rollback();
						throw;
					}
				}
			}
			return deleted;
		}
	}
}
