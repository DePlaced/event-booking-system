using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32.SafeHandles;
using Data.Exceptions;
using Data.ModelLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
	public class EventAccess : IEventAccess
	{
		// The con string used to connect the database.
		private readonly string? _connectionString;

		// Initializes a new instance of the EventAccess class.
		public EventAccess(IConfiguration config)
		{
			// Retrieve the con string from the configuration using the "ConnectionStrings" key.
			_connectionString = config.GetConnectionString("SignRental");
		}

		/// <summary>
		/// Gets a specific Event from the database based on the given ID.
		/// </summary>
		public async Task<Event?> GetEventById(int id)
		{
			Event? @event;
			// Defines query string
			string queryString = "SELECT id, eventName, eventDate, eventDescription, price, availabilityStatus, signId, rowId, cast(rowId as bigint) as rowIdBig FROM Event WHERE id = @Id;";
			// Use a SqlConnection to connect to the database.
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				// Returns null if nothing is found
				IEnumerable<Event> enumerable = await con.QueryAsync<Event>(queryString, new { Id = id });
				@event = enumerable.FirstOrDefault();
			}
			return @event;
		}

		/// <summary>
		/// Gets all events associated with signId.
		/// </summary>
		public async Task<List<Event>> GetAllEventsBySignId(int signId)
		{
			List<Event> events;
			// Defines query string.
			var queryString = "SELECT id, eventName, eventDate, eventDescription, price, availabilityStatus, signId, rowId, cast(rowId as bigint) as rowIdBig FROM [Event] WHERE signId = @SignId;";
			// Use a SqlConnection to connect to the database.
			using (var con = new SqlConnection(_connectionString))
			{
				// Query retrieves multiple rows from the database and map them to a list of Event objects.
				// The result is converted to a List and returned.
				IEnumerable<Event> enumerable = await con.QueryAsync<Event>(queryString, new { SignId = signId });
				events = enumerable.ToList();
			}
			return events;
		}

		/// <summary>
		/// Gets all Events from the database.
		/// </summary>
		public async Task<List<Event>> GetAllEvents()
		{
			List<Event> events;
			// Defines query string.
			var queryString = "SELECT id, eventName, eventDate, eventDescription, price, availabilityStatus, signId, rowId, cast(rowId as bigint) as rowIdBig FROM [Event];";
			// Use a SqlConnection to connect to the database.
			using (var con = new SqlConnection(_connectionString))
			{
				// Query retrieves multiple rows from the database and map them to a list of Event objects.
				// The result is converted to a List and returned.
				IEnumerable<Event> enumerable = await con.QueryAsync<Event>(queryString);
				events = enumerable.ToList();
			}
			return events;
		}

		/// <summary>
		/// Adds a new Event to the Event table in the database.
		/// </summary>
		public async Task<int> CreateEvent(Event eventToAdd)
		{
			int insertedId;
			// Defines query string
			string queryString =
				"INSERT INTO [Event] (eventName, eventDate, eventDescription, price, availabilityStatus, signId) " +
				"OUTPUT INSERTED.ID " +
				"VALUES (@EventName, @EventDate, @EventDescription, @Price, @AvailabilityStatus, @SignId);";
			// Use a SqlConnection to connect to the database.
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				// ExecuteScalar is used to retrieve a single value from the database.
				insertedId = await con.ExecuteScalarAsync<int>(queryString, eventToAdd);
			}
			// Return the ID of the newly created booking record.
			return insertedId;
		}

		/// <summary>
		/// Updates the Event with a specific ID in the database.
		/// </summary>
		public async Task<bool> UpdateEvent(int id, Event eventToUpdate)
		{
			bool updated = false;
			// Define query string
			string queryString = "UPDATE [Event] SET eventName = @EventName, eventDate = @EventDate, eventDescription = @EventDescription, price = @Price, availabilityStatus = @AvailabilityStatus, signId = @SIgnId WHERE id = @Id AND (cast(@RowIdBig as binary(8)) = rowId);";
			// Creates new object with given ID.
			var eventWithId = new Event(id, eventToUpdate.EventName, eventToUpdate.EventDate, eventToUpdate.EventDescription, eventToUpdate.Price, eventToUpdate.AvailabilityStatus, eventToUpdate.SignId, eventToUpdate.RowId, eventToUpdate.RowIdBig);
			// Use a SqlConnection to connect to the database.
			using (var con = new SqlConnection(_connectionString))
			{
				await con.OpenAsync();

				using (SqlTransaction transaction = con.BeginTransaction(IsolationLevel.ReadUncommitted))
				{
					try
					{
						// Returns the number of affected rows.
						int numberOfUpdatedRows = await con.ExecuteAsync(queryString, eventWithId, transaction);

						if (numberOfUpdatedRows > 0)
						{
							updated = true;
							transaction.Commit();
						}
						else
						{
							throw new BadRequestException("Event has been booked.");
						}
					}
					catch (Exception)
					{
						transaction.Rollback();
						throw;
					}
				}
			}
			return updated;
		}

		/// <summary>
		/// Deletes an Event with the given ID.
		/// </summary>
		public async Task<bool> DeleteEventById(int id)
		{
			bool deletedSuccess = false;
			// Define query string
			string queryString = "DELETE FROM [Event] WHERE id = @Id";

			// Use a SqlConnection to connect to the database.
			using (var con = new SqlConnection(_connectionString))
			{
				try
				{
					//sets true if event was deleted succesful
					deletedSuccess = await con.ExecuteAsync(queryString, new { Id = id }) > 0;
				}
				catch (SqlException ex)
				{
					if (ex.Number == 547) // Foreign key violation error number
					{
						throw new BadRequestException("Event has been booked.");
					}
					else
					{
						// Handle other SQL exceptions or re-throw the exception
						throw;
					}
				}
			}
			return deletedSuccess;
		}
	}
}



