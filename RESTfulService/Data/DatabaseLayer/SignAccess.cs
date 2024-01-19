using Dapper;
using Microsoft.Extensions.Configuration;
using Data.ModelLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
	public class SignAccess : ISignAccess

	{
		// The con string used to connect the database.
		private readonly string? _connectionString;

		// Initializes a new instance of the EventAccess class.
		public SignAccess(IConfiguration config)
		{
			// Retrieve the con string from the configuration using the "SignRental" key.
			_connectionString = config.GetConnectionString("SignRental");
		}

		/// <summary>
		/// Gets a specific sign from the database based on the id.
		/// </summary>
		public async Task<Sign?> GetSignById(int id)
		{
			Sign? sign;
			// Defines query string.
			string queryString = "SELECT id, size, resolution, signLocation, stadiumId FROM Sign WHERE id = @Id;";
			// Use a SqlConnection to connect to the database.
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				// Returns null if nothing found.
				IEnumerable<Sign> enumerable = await con.QueryAsync<Sign>(queryString, new { Id = id });
				sign = enumerable.FirstOrDefault();
			}
			return sign;
		}

		/// <summary>
		/// Gets all signs from the database.
		/// </summary>
		public async Task<List<Sign>> GetAllSigns(int stadiumId)
		{
			List<Sign> signs;
			// Defines query string.
			var queryString = "SELECT id, size, resolution, signLocation, stadiumId FROM Sign WHERE stadiumId = @StadiumId;";
			// Use a SqlConnection to connect to the database.
			using (var con = new SqlConnection(_connectionString))
			{
				// Query retrieves multiple rows from the database and map them to a list of Booking objects.
				// The result is converted to a List and returned.
				IEnumerable<Sign> enumerable = await con.QueryAsync<Sign>(queryString, new { StadiumId = stadiumId });
				signs = enumerable.ToList();
			}
			return signs;
		}

		/// <summary>
		/// Adds a new sign row to the sign table in the database.
		/// </summary>
		public async  Task<int> CreateSign(Sign signToAdd)
		{
			int insertedId;
			// Defines query string.
			string queryString =
				"INSERT INTO [Sign] (size, resolution, signLocation, stadiumId) " +
				"OUTPUT INSERTED.ID " +
				"VALUES(@Size, @Resolution, @SignLocation, @StadiumId);";
			// Use a SqlConnection to connect to the database.
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				// ExecuteScalar is used to retrieve a single value from the database.
				insertedId = await connection.ExecuteScalarAsync<int>(queryString, signToAdd);
			}
			// Return the ID of the newly created Sign record.
			return insertedId;
		}

		/// <summary>
		/// Updates the Sign with a specific ID in the database.
		/// </summary>
		public async Task<bool> UpdateSign(int id, Sign signToUpdate)
		{
			bool updated;
			// Define query string
			string queryString = "UPDATE Sign SET size = @Size, resolution = @Resolution, signLocation = @SignLocation, stadiumId = @StadiumId WHERE id = @Id;";
			// Creates new object with given id.
			var signWithId = new Sign(id, signToUpdate.Size, signToUpdate.Resolution, signToUpdate.SignLocation, signToUpdate.StadiumId);
			// Use a SqlConnection to connect to the database.
			using (var con = new SqlConnection(_connectionString))
			{
				//Returns the number of affected records, and the method returns true if at least one record is affected.
				updated = await con.ExecuteAsync(queryString, signWithId) > 0;
			}
			return updated;
		}

		/// <summary>
		/// Deletes a Sign with the given ID.
		/// </summary>
		public async Task<bool> DeleteSignById(int id)
		{
			bool deleted;
			// Define query string
			string queryString = "DELETE FROM Sign WHERE id = @Id;";
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
