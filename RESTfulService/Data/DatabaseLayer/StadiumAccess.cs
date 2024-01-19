using Dapper;
using Microsoft.Extensions.Configuration;
using SignData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace SignData.DatabaseLayer
{
	public class StadiumAccess : IStadiumAccess
	{
		// The con string used to connect the database.
		private readonly string? _connectionString;

		// Initializes a new instance of the EventAccess class.
		public StadiumAccess(IConfiguration config)
		{
			// Retrieve the con string from the configuration using the "SignRental" key.
			_connectionString = config.GetConnectionString("SignRental");
		}

		/// <summary>
		/// Adds a new Stadium to the Stadium table in the database, also creates a address row and a zipcode row, if it doesnt aldready exist.
		/// </summary>
		public async Task<int> CreateStadium(Stadium stadiumToAdd)
		{
			// Arrange variables.
			int insertedId = -1;
			string queryString = @"DECLARE @ExistingZipcode INT; " +
				"DECLARE @AddressId INT; " +
				"SELECT @ExistingZipcode = zipcode FROM Zipcode WHERE zipcode = @Zipcode; " +
				"IF @ExistingZipcode IS NOT NULL " +
				"BEGIN " +
				"INSERT INTO [Address] (aAddress, zipcode) VALUES (@Street, @ExistingZipcode); " +
				"SET @AddressId = SCOPE_IDENTITY(); " +
				"INSERT INTO Stadium (stadiumName, addressId, adminId) OUTPUT INSERTED.Id AS Id VALUES (@StadiumName, @AddressId, @AdminId);" +
				"END " +
				"ELSE " +
				"BEGIN " +
				"INSERT INTO Zipcode (zipcode, city) VALUES (@Zipcode, @City); " +
				"INSERT INTO [Address] (aAddress, zipcode) VALUES (@Street, @Zipcode); " +
				"SET @AddressId = SCOPE_IDENTITY(); " +
				"INSERT INTO Stadium (stadiumName, addressId, adminId) OUTPUT INSERTED.Id AS Id VALUES (@StadiumName, @AddressId, @AdminId);" +
				"END;";
			// Use a SqlConnection to connect to the database.
			using (var con = new SqlConnection(_connectionString))
			{
				// Insert Stadium as well as rows in zipcode and address.
				insertedId = await con.ExecuteScalarAsync<int>(queryString, stadiumToAdd);
			}
			// Return the ID of the newly created Stadium record.
			return insertedId;
		}

		/// <summary>
		/// Deletes a Stadium from Stadium table with the given ID.
		/// </summary>
		public async Task<bool> DeleteStadiumById(int id)
		{
			bool deleted;
			// Define query string
			string queryString = "DELETE FROM Stadium WHERE id = @Id";
			// Use a SqlConnection to connect to the database.
			using (var con = new SqlConnection(_connectionString))
			{
				// Returns the number of affected records, and the method returns true if at least one record is affected.
				deleted = await con.ExecuteAsync(queryString, new { Id = id }) > 0;
			}
			return deleted;
		}

		// <summary>
		// Returns all the Stadiums in the Stadium table, with joined attributes from the Address and Zipcode table.
		// </summary>
		public async Task<List<Stadium>> GetAllStadiums()
		{
			List<Stadium> stadiums;
		   // Defines query string
		   var queryString = @"
				SELECT s.id, s.stadiumName, a.aAddress AS street, z.city, a.zipcode, s.adminId
				FROM Stadium s
				JOIN Address a ON s.addressId = a.id
				JOIN Zipcode z ON a.zipcode = z.zipcode;";
			// Use a SqlConnection to connect to the database.
			using (var con = new SqlConnection(_connectionString))
			{
				// Query retrieves multiple rows from the database and map them to a list of Stadium objects.
				// The result is converted to a List and returned.
				IEnumerable<Stadium> enumerable = await con.QueryAsync<Stadium>(queryString);
				stadiums = enumerable.ToList();
			}
			return stadiums;
		}

		/// <summary>
		/// Returns a Stadium with the given ID.
		/// </summary>
		public async Task<Stadium?> GetStadiumById(int id)
		{
			Stadium? stadium;
			// Defines query string.
			string queryString = @"
				SELECT s.id, s.stadiumName, a.aAddress AS street, z.city, a.zipcode, s.adminId
				FROM Stadium s
				JOIN Address a ON s.addressId = a.id
				JOIN Zipcode z ON a.zipcode = z.zipcode
				WHERE s.id = @Id;";
			// Use a SqlConnection to connect to the database.
			using (var con = new SqlConnection(_connectionString))
			{
				// Returns null if nothing is found.
				IEnumerable<Stadium> enumerable = await con.QueryAsync<Stadium>(queryString, new { Id = id });
				stadium = enumerable.FirstOrDefault();
			}
			return stadium;
		}

		/// <summary>
		/// Updates the Stadium with a specific ID in the database.
		/// </summary>
		public async Task<bool> UpdateStadium(int id, Stadium stadiumToUpdate)
		{
			bool updated;
			// Defines query string.
			string queryString = @"DECLARE @ExistingZipcode INT; " +
				"SELECT @ExistingZipcode = zipcode FROM Zipcode WHERE zipcode = @Zipcode; " +
				"IF @ExistingZipcode IS NOT NULL " +
				"BEGIN " +
				"UPDATE Stadium SET stadiumName = @StadiumName WHERE id = @Id; " +
				"UPDATE [Address] SET aAddress = @Street, zipcode = @Zipcode WHERE Id = (SELECT addressId FROM Stadium WHERE id = @Id); " +
				"END " +
				"ELSE " +
				"BEGIN " +
				"INSERT INTO Zipcode (zipcode, city) VALUES (@Zipcode, @City); " +
				"UPDATE Stadium SET stadiumName = @StadiumName WHERE id = @Id; " +
				"UPDATE [Address] SET aAddress = @Street, zipcode = @Zipcode WHERE Id = (SELECT addressId FROM Stadium WHERE id = @Id); " +
				"END;";
			// Creates new object with given id.
			var stadiumWithId = new Stadium(id, stadiumToUpdate.StadiumName, stadiumToUpdate.Street, stadiumToUpdate.City, stadiumToUpdate.Zipcode, stadiumToUpdate.AdminId);
			// Use a SqlConnection to connect to the database.
			using (var con = new SqlConnection(_connectionString))
			{
				// Assuming stadiumToUpdate contains parameters needed for the query.
				// Set parameters here using command.Parameters.AddWithValue()
				int insertedRows = await con.ExecuteAsync(queryString, stadiumWithId);
				// Returns the number of affected records, and the method returns true if at least one record is affected.
				updated = insertedRows > 0;
			}
			return updated;
		}
	}
}
