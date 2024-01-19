using RESTfulService.DTOs;
using RESTfulService.ModelConversion;
using Data.DatabaseLayer;
using Data.Exceptions;
using Data.ModelLayer;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace RESTfulService.BusinesslogicLayer
{
	/// <summary>
	/// Service class for handling operations related to user entities.
	/// </summary>
	public class UserLogic : IUserLogic
	{
		/// <summary>
		/// The data access layer for user entities.
		/// </summary>
		private readonly IUserAccess _userAccess;
		// TODO: Put in database or config.
		private readonly int _iterations = 10000;

		/// <summary>
		/// Initializes a new instance of the UserService class.
		/// </summary>
		/// <param name="userAccess">The user access object used for database operations.</param>
		public UserLogic(IUserAccess userAccess)
		{
			_userAccess = userAccess;
		}

		/// <summary>
		/// Authenticate an user from their username and password.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns>The authenticated user, or null if the user could not be authenticated.</returns>
		public UserDTO? Authenticate(string username, string password)
		{
			if (string.IsNullOrWhiteSpace(username))
			{
				throw new BadRequestException("Username cannot be empty.");
			}
			if (string.IsNullOrWhiteSpace(password))
			{
				throw new BadRequestException("Password cannot be empty.");
			}

			bool authenticated;
			User? user = _userAccess.GetUserByUsername(username);
			// Authenticate the user by verifying their password
			if (user is not null && user.LoginCredentials is not null && VerifyPassword(password, user.LoginCredentials.Salt, _iterations, user.LoginCredentials.PasswordHash))
			{
				authenticated = true;
			}
			else
			{
				authenticated = false;
			}
			return authenticated ? user!.ToUserDTO() : null;
		}

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="userDTO">The user to register.</param>
        /// <returns>The inserted ID.</returns>
        public int Register(UserDTO userDTO)
		{
			if (string.IsNullOrWhiteSpace(userDTO.Username))
			{
				throw new BadRequestException("Username cannot be empty.");
			}
			if (string.IsNullOrWhiteSpace(userDTO.Password))
			{
				throw new BadRequestException("Password cannot be empty.");
			}
			if (_userAccess.GetUserByUsername(userDTO.Username) is not null)
			{
				throw new BadRequestException("Username is already taken.");
			}

			// Generate a random salt to be used with the password hash.
			// The salt will also be stored in the database so it can be used to verify the password.
			byte[] salt = GenerateSalt();
			// Hash the pasword with salt.
			string passwordHash = HashPassword(userDTO.Password, salt, _iterations);
			// Create login credentials with containing the hashed password and salt.
			LoginCredentials loginCredentials = new LoginCredentials(-1, userDTO.Username, passwordHash, salt);
			User user = new User(-1, userDTO.FirstName, userDTO.LastName, userDTO.Email, loginCredentials);
			// Store the user in the database and retrieve the inserted user ID.
			int insertedId = _userAccess.CreateUser(user);
			return insertedId;
		}

        /// <summary>
        /// Hash a password with salt using PBKDF2 and SHA512.
        /// </summary>
        /// <param name="password">The password to be hashed.</param>
        /// <param name="salt">The salt to hash with.</param>
        /// <param name="iterations">The number of iterations for the hash.</param>
        /// <returns>The hashed password.</returns>
        private string HashPassword(string password, byte[] salt, int iterations)
		{
			byte[] passwordHash;
			// Hash the password with salt using PBKDF2 and SHA512.
			using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA512))
			{
				// 32 bytes is a common length for a secure hash.
				passwordHash = pbkdf2.GetBytes(32);
			}
			return Convert.ToBase64String(passwordHash);
		}

		/// <summary>
		/// Generate a random salt.
		/// </summary>
		private byte[] GenerateSalt()
		{
			// 16 bytes is a common length for salt.
			byte[] salt = new byte[16];
			using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
			{
				// Fill the salt with random values.
				rng.GetBytes(salt);
			}
			return salt;
		}

        /// <summary>
        /// Verify a password by hashing it and comparing it to a stored password hash.
        /// </summary>
        /// <param name="password">The password to verify</param>
        /// <param name="salt">The salt to hash with.</param>
        /// <param name="iterations">The number of iterations for the hash.</param>
        /// <param name="storedPasswordHash">The stored password hash to compare with.</param>
        /// <returns>True if the hashes are equal, false if not.</returns>
        private bool VerifyPassword(string password, byte[] salt, int iterations, string storedPasswordHash)
		{
			return HashPassword(password, salt, iterations) == storedPasswordHash;
		}
	}
}
