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
	public class UserAccess : IUserAccess
	{
		private readonly string? _connectionString;

		public UserAccess(IConfiguration config)
		{
			_connectionString = config.GetConnectionString("SignRental");
		}

		/// <summary>
		/// Gets an user by ID.
		/// </summary>
		/// <param name="id">The user ID.</param>
		/// <returns>The found user object, or null if nothing was found.</returns>
		public User? GetUserById(int id)
		{
			User? user;
			// Defines query string
			string queryString =
				"SELECT [User].id, [User].firstName, [User].lastName, [User].email, UserRole.id, UserRole.role, LoginCredentials.id, LoginCredentials.username, LoginCredentials.passwordHash, LoginCredentials.salt " +
				"FROM [User] " +
				"JOIN UserRole ON [User].userRoleId = UserRole.id " +
				"JOIN LoginCredentials ON [User].loginCredentialsId = LoginCredentials.id " +
				"WHERE [User].id = @Id;";
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				// Returns null if nothing found.
				user = connection.Query<User, UserRole, LoginCredentials, User>(
					queryString,
					param: new { Id = id },
					// Split on User.id, UserRole.id and LoginCredentials.id
					splitOn: "id,id,id",
					map: (user, userRole, loginCredentials) =>
					{
						// Map the user role property.
						user.UserRole = userRole;
						// Map the login credentials property.
						user.LoginCredentials = loginCredentials;
						// Return the mapped user.
						return user;
					}
				).FirstOrDefault();
			}
			return user;
		}

		/// <summary>
		/// Gets an user by username.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <returns>The found user object, or null if nothing was found.</returns>
		public User? GetUserByUsername(string username)
		{
			User? user;
			// Defines query string
			string queryString =
				"SELECT [User].id, [User].firstName, [User].lastName, [User].email, UserRole.id, UserRole.role, LoginCredentials.id, LoginCredentials.username, LoginCredentials.passwordHash, LoginCredentials.salt " +
				"FROM [User] " +
				"JOIN UserRole ON [User].userRoleId = UserRole.id " +
				"JOIN LoginCredentials ON [User].loginCredentialsId = LoginCredentials.id " +
				"WHERE LoginCredentials.username = @Username;";
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				// Returns null if nothing found.
				user = connection.Query<User, UserRole, LoginCredentials, User>(
					queryString,
					param: new { Username = username },
					// Split on User.id, UserRole.id and LoginCredentials.id.
					splitOn: "id,id,id",
					map: (user, userRole, loginCredentials) =>
					{
						// Map the user role property.
						user.UserRole = userRole;
						// Map the login credentials property.
						user.LoginCredentials = loginCredentials;
						// Return the mapped user.
						return user;
					}
				).FirstOrDefault();
			}
			return user;
		}

		/// <summary>
		/// Creates a new user from an user object.
		/// </summary>
		/// <param name="user">The user object.</param>
		/// <returns>The ID of the created user.</returns>
		public int CreateUser(User user)
		{
			int insertedUserId;
			string insertLoginCredentialsSql =
				"INSERT INTO LoginCredentials (username, passwordHash, salt)" +
				"OUTPUT INSERTED.ID " +
				"VALUES (@Username, @PasswordHash, @Salt)";
			string insertUserSql =
				"INSERT INTO [User] (firstName, lastName, email, loginCredentialsId) " +
				"OUTPUT INSERTED.ID " +
				"VALUES (@FirstName, @LastName, @Email, @LoginCredentialsId);";
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				// Begin a transaction.
				using (SqlTransaction transaction = connection.BeginTransaction())
				{
					try
					{
						// Insert login credentials if not null.
						int? insertedLoginCredentialsId = user.LoginCredentials is not null ? connection.ExecuteScalar<int>(insertLoginCredentialsSql, user.LoginCredentials, transaction) : null;
						// Insert user along with the inserted login credentials ID.
						insertedUserId = connection.ExecuteScalar<int>(insertUserSql, new { FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, LoginCredentialsId = insertedLoginCredentialsId }, transaction);
						// Commit the transaction if both inserts were successful.
						transaction.Commit();
					}
					catch
					{
						// Rollback the transaction if anything went wrong.
						transaction.Rollback();
						throw;
					}
				}
			}
			return insertedUserId;
		}

		/// <summary>
		/// Gets an user role by ID.
		/// </summary>
		/// <param name="id">The user ID.</param>
		/// <returns>The user role.</returns>
		public UserRole? GetUserRoleById(int id)
		{
			UserRole? userRole;
			// Defines query string.
			string queryString = "SELECT id, role FROM UserRole WHERE id = @Id;";
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				// Returns null if nothing found.
				userRole = connection.Query<UserRole>(queryString, new { Id = id }).FirstOrDefault();
			}
			return userRole;
		}
	}
}
