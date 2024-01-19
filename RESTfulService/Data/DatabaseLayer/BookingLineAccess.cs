using Dapper;
using Microsoft.Extensions.Configuration;
using Data.ModelLayer;
using System.Data.SqlClient;
using System.Data;

namespace Data.DatabaseLayer
{
	public class BookingLineAccess : IBookingLineAccess
	{

		private readonly string? _connectionString;

		public BookingLineAccess(IConfiguration config)
		{
			_connectionString = config.GetConnectionString("SignRental");
		}

		/// <summary>
		/// Find BookingLine on id
		/// </summary>
		public async Task<BookingLine?> GetBookingLineById(int id)
		{
			BookingLine bookingLine;
			string queryString = "SELECT id, subPrice, bookingId, eventId FROM BookingLine WHERE id = @Id;";

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				IEnumerable<BookingLine> enumerable = await connection.QueryAsync<BookingLine>(queryString, new { Id = id });
				bookingLine = enumerable.First();
			}
			return bookingLine;
		}

		/// <summary>
		/// Fijd bookingLines on bookingId
		/// </summary>
		public async Task<List<BookingLine>> GetBookingLinesByBookingId(int bookingId)
		{
			List<BookingLine> bookingLines;
			var queryString = "SELECT id, subPrice, bookingId, eventId FROM BookingLine Where bookingId = @BookingId;";

			using (var connection = new SqlConnection(_connectionString))
			{
				IEnumerable<BookingLine> enumerable = await connection.QueryAsync<BookingLine>(queryString, new { BookingId = bookingId });
				bookingLines = enumerable.ToList();
			}
			return bookingLines;
		}

		/// <summary>
		/// Update bookingLine with id
		/// </summary>
		public async Task<bool> UpdateBookingLine(BookingLine bookingLineToUpdate)
		{
			bool updated;
			string queryString = "UPDATE BookingLine SET subPrice = @SubPrice, bookingId = @BookingId, eventId = @EventId WHERE id = @Id;";

			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				using (var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted))
				{
					try
					{
						updated = await connection.ExecuteAsync(queryString, bookingLineToUpdate) > 0;
						await transaction.CommitAsync();
					}
					catch (Exception ex)
					{
						await transaction.RollbackAsync();
						throw new Exception("Failed to delete bookingLine", ex);
					}
				}

			}
			return updated;
		}

		/// <summary>
		/// Deletes BookingLine with id
		/// </summary>
		public async Task<bool> DeleteBookingLine(int id)
		{
			bool deleted;
			string queryString = "DELETE FROM Booking WHERE id = @Id;";

			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				using (var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted))
				{
					try
					{
						deleted = await connection.ExecuteAsync(queryString, new { Id = id }) > 0;
						await transaction.CommitAsync();
					}
					catch (Exception ex)
					{
						await transaction.RollbackAsync();
						throw new Exception("Failed to delete bookingLine", ex);
					}
				}
					
			}
			return deleted;
		}
	}
}
