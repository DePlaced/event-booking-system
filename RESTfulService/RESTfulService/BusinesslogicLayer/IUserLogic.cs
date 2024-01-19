using System;
using RESTfulService.DTOs;

namespace RESTfulService.BusinesslogicLayer
{
	public interface IUserLogic
	{
		/// <summary>
		/// Authenticate an user from their username and password
		/// </summary>
		UserDTO? Authenticate(string username, string password);

		/// <summary>
		/// Register a new user
		/// </summary>
		int Register(UserDTO userDTO);
	}
}
